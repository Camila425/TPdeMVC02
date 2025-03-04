using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;
using TPDeMVC02.Web.ViewModels.Genres;
using X.PagedList.Extensions;

namespace TPDeMVC02.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GenresController : Controller
    {
        private readonly IGenreServicio? _genreServicio;
        private readonly IMapper? _mapper;

        public GenresController(IGenreServicio? genreServicio, IMapper? mapper)
        {
            _genreServicio = genreServicio;
            _mapper = mapper;
        }

        public IActionResult Index(int? page)
        {
            int PageNumber = page ?? 1;
            int PageSize = 10;
            var Genres = _genreServicio?.GetAll(orderBy: o => o.OrderBy(g => g.GenreName));
            var GenresVm = _mapper?.Map<List<GenreEditVm>>(Genres).ToPagedList(PageNumber, PageSize); ;
            return View(GenresVm);
        }

        public IActionResult UpSert(int? id)
        {
            if (_genreServicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Dependencies are not configured correctly");
            }
            GenreEditVm genreEditVm;
            if (id is null || id == 0)
            {
                genreEditVm = new GenreEditVm();
            }
            else
            {
                try
                {
                    Genre? genre = _genreServicio.Get(filter: g => g.GenreId == id);
                    if (genre == null)
                    {
                        return NotFound();
                    }
                    genreEditVm = _mapper.Map<GenreEditVm>(genre);
                    return View(genreEditVm);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "An error occurred while record");
                }
            }
            return View(genreEditVm);

        }

        [HttpPost]
        public IActionResult UpSert(GenreEditVm genreEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(genreEditVm);
            }

            if (_genreServicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Dependencies are not configured correctly");
            }
            try
            {
                Genre genre = _mapper.Map<Genre>(genreEditVm);
                if (_genreServicio.Exist(genre))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(genreEditVm);
                }
                _genreServicio.Save(genre);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while record");
                return View(genreEditVm);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Genre? genre = _genreServicio?.Get(filter: g => g.GenreId == id);
            if (genre is null)
            {
                return NotFound();
            }
            try
            {
                if (_genreServicio == null || _mapper == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       "Dependencies are not configured correctly");
                }

                if (_genreServicio.ItsRelated(genre.GenreId))
                {
                    return Json(new { success = false, message = "Related Record...Delete Deny!!" });

                }
                _genreServicio.Delete(genre);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete record!!!" }); ;

            }
        }

    }
}
