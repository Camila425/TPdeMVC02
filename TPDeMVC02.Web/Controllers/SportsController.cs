using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;
using TPDeMVC02.Web.ViewModels.Sports;
using X.PagedList.Extensions;

namespace TPDeMVC02.Web.Controllers
{
    public class SportsController : Controller
    {
        private readonly ISportServicio? _sportServicio;
        private readonly IMapper? _mapper;

        public SportsController(ISportServicio? sportServicio, IMapper? mapper)
        {
            _sportServicio = sportServicio;
            _mapper = mapper;
        }

        public IActionResult Index(int? page, string? searchTerm = null, bool viewAll = false, int pageSize = 10)
        {
            int PageNumber = page ?? 1;
            ViewBag.currentPageSize = pageSize;
            IEnumerable<Sport>? Sports;
            if (!viewAll)
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    Sports = _sportServicio?.GetAll(orderBy: o => o.OrderBy(s => s.SportName), 
                        filter: s => s.SportName.Contains(searchTerm));
                    ViewBag.currentSearchTerm = searchTerm;
                }
                else
                {
                    Sports = _sportServicio?.GetAll(orderBy: o => o.OrderBy(s => s.SportName));
                }
            }
            else
            {
                Sports = _sportServicio?.GetAll(orderBy: o => o.OrderBy(s => s.SportName));
            }
            var SportsVm = _mapper?.Map<List<SportListVm>>(Sports).ToPagedList(PageNumber, pageSize);
            return View(SportsVm);

        }
        public IActionResult UpSert(int? id)
        {
            if (_sportServicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Dependencies are not configured correctly");
            }
            SportEditVm sportEditVm;
            if (id is null || id == 0)
            {
                sportEditVm = new SportEditVm();
            }
            else
            {
                try
                {
                    Sport? sport = _sportServicio.Get(filter: s => s.SportId == id);
                    if (sport == null)
                    {
                        return NotFound();
                    }
                    sportEditVm = _mapper.Map<SportEditVm>(sport);
                    return View(sportEditVm);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,"An error occurred while record");
                }
            }
            return View(sportEditVm);

        }

        [HttpPost]
        public IActionResult UpSert(SportEditVm sportEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(sportEditVm);
            }

            if (_sportServicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Dependencies are not configured correctly");
            }
            try
            {
                Sport sport = _mapper.Map<Sport>(sportEditVm);
                if (_sportServicio.Exist(sport))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(sportEditVm);
                }
                _sportServicio.Save(sport);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while record");
                return View(sportEditVm);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Sport? sport = _sportServicio?.Get(filter: s => s.SportId == id);
            if (sport is null)
            {
                return NotFound();
            }
            try
            {
                if (_sportServicio == null || _mapper == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       "Dependencies are not configured correctly");
                }

                if (_sportServicio.ItsRelated(sport.SportId))
                {
                    return Json(new { success = false, message = "Related Record...Delete Deny!!" });

                }
                _sportServicio.Delete(sport);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete record!!!" }); ;

            }
        }
    }
}
