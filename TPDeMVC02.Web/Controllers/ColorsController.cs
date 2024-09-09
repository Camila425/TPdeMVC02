using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;
using TPDeMVC02.Web.ViewModels.Colors;
using X.PagedList.Extensions;

namespace TPDeMVC02.Web.Controllers
{
    public class ColorsController : Controller
    {
        private readonly IColorServicio? _colorServicio;
        private readonly IMapper? _mapper;

        public ColorsController(IColorServicio? ColorServicio, IMapper? mapper)
        {
            _colorServicio = ColorServicio;
            _mapper = mapper;
        }
        public IActionResult Index(int? page, string? searchTerm = null, bool viewAll = false, int pageSize = 10)
        {
            int PageNumber = page ?? 1;
            ViewBag.currentPageSize = pageSize;
            IEnumerable<Color>? colors;
            if (!viewAll)
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    colors = _colorServicio?.GetAll(orderBy: o => o.OrderBy(c => c.ColorName),
                        filter: c => c.ColorName.Contains(searchTerm));
                    ViewBag.currentSearchTerm = searchTerm;
                }
                else
                {
                    colors = _colorServicio?.GetAll(orderBy: o => o.OrderBy(c => c.ColorName));
                }
            }
            else
            {
                colors = _colorServicio?.GetAll(orderBy: o => o.OrderBy(c => c.ColorName));
            }
            var colorsVm = _mapper?.Map<List<ColorListVm>>(colors).ToPagedList(PageNumber, pageSize);
            return View(colorsVm);

        }
        public IActionResult UpSert(int? id)
        {
            if (_colorServicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Dependencies are not configured correctly");
            }
            ColorEditVm colorEditVm;
            if (id is null || id == 0)
            {
                colorEditVm = new ColorEditVm();
            }
            else
            {
                try
                {
                    Color? color = _colorServicio.Get(filter: c => c.ColorId == id);
                    if (color == null)
                    {
                        return NotFound();
                    }
                    colorEditVm = _mapper.Map<ColorEditVm>(color);
                    return View(colorEditVm);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "An error occurred while retrieving the record");

                }
            }
            return View(colorEditVm);

        }

        [HttpPost]
        public IActionResult UpSert(ColorEditVm colorEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(colorEditVm);
            }

            if (_colorServicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Dependencies are not configured correctly");
            }
            try
            {
                Color color = _mapper.Map<Color>(colorEditVm);
                if (_colorServicio.Exist(color))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(colorEditVm);
                }
                _colorServicio.Save(color);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record");
                return View(colorEditVm);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Color? color = _colorServicio?.Get(filter: c => c.ColorId == id);
            if (color is null)
            {
                return NotFound();
            }
            try
            {
                if (_colorServicio == null || _mapper == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       "Dependencies are not configured correctly");
                }

                if (_colorServicio.ItsRelated(color.ColorId))
                {
                    return Json(new { success = false, message = "Related Record...Delete Deny!!" });

                }
                _colorServicio.Delete(color);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete record!!!" }); ;

            }
        }
    }
}
