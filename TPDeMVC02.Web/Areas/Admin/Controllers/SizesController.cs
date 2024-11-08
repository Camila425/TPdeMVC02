using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;
using TPDeMVC02.Web.ViewModels.Sizes;
using X.PagedList.Extensions;

namespace TPDeMVC02.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizesController : Controller
    {
        private readonly ISizeServicio? _sizeServicio;
        private readonly IMapper? _mapper;

        public SizesController(ISizeServicio? sizeServicio, IMapper? mapper)
        {
            _sizeServicio = sizeServicio;
            _mapper = mapper;
        }
        public IActionResult Index(int? page)
        {
            int PageNumber = page ?? 1;
            int PageSize = 5;
            var Sizes = _sizeServicio?.GetAll(orderBy: s => s.OrderBy(s => s.SizeNumber));
            var SizesVm = _mapper?.Map<List<SizeEditVm>>(Sizes).ToPagedList(PageNumber, PageSize); ;
            return View(SizesVm);
        }
        public IActionResult UpSert(int? id)
        {
            if (_sizeServicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Dependencies are not configured correctly");
            }
            SizeEditVm sizesEditVm;
            if (id is null || id == 0)
            {
                sizesEditVm = new SizeEditVm();
            }
            else
            {
                try
                {
                    Size? size = _sizeServicio.Get(filter: s => s.SizeId == id);
                    if (size == null)
                    {
                        return NotFound();
                    }
                    sizesEditVm = _mapper.Map<SizeEditVm>(size);
                    return View(sizesEditVm);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "An error occurred while record");

                }
            }
            return View(sizesEditVm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(SizeEditVm sizesEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(sizesEditVm);
            }

            if (_sizeServicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Dependencies are not configured correctly");
            }
            try
            {
                Size size = _mapper.Map<Size>(sizesEditVm);
                if (_sizeServicio.Exist(size))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(sizesEditVm);
                }
                _sizeServicio.Save(size);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while record");
                return View(sizesEditVm);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Size? size = _sizeServicio?.Get(filter: s => s.SizeId == id);
            if (size is null)
            {
                return NotFound();
            }
            try
            {
                if (_sizeServicio == null || _mapper == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       "Dependencies are not configured correctly");
                }

                if (_sizeServicio.ItsRelated(size.SizeId))
                {
                    return Json(new { success = false, message = "Related Record...Delete Deny!!" });

                }
                _sizeServicio.Delete(size);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete record!!!" }); ;

            }
        }
    }
}
