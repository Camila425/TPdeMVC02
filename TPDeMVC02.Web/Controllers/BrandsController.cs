using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;
using TPDeMVC02.Web.ViewModels.Brands;
using X.PagedList.Extensions;

namespace TPDeMVC02.Web.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IBrandService? _BrandService;
        private readonly IMapper? _mapper;
        public BrandsController(IBrandService? BrandService, IMapper mapper)
        {
            _BrandService = BrandService;
            _mapper = mapper;
        }
        public IActionResult Index(int? page, string? searchTerm = null, bool viewAll = false, int pageSize = 10)
        {
            int PageNumber = page ?? 1;
            ViewBag.currentPageSize = pageSize;
            IEnumerable<Brand>? brands;
            if (!viewAll)
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    brands = _BrandService?.GetAll(orderBy: o => o.OrderBy(b => b.BrandName),
                        filter: b => b.BrandName.Contains(searchTerm));
                    ViewBag.currentSearchTerm = searchTerm;
                }
                else
                {
                    brands = _BrandService?.GetAll(orderBy: o => o.OrderBy(b => b.BrandName));
                }
            }
            else
            {
                brands = _BrandService?.GetAll(orderBy: o => o.OrderBy(b => b.BrandName));
            }
            var brandsVm = _mapper?.Map<List<BrandListVm>>(brands).ToPagedList(PageNumber, pageSize);
            return View(brandsVm);

        }
        public IActionResult UpSert(int? id)
        {
            if (_BrandService == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Dependencies are not configured correctly");
            }
            BrandEditVm brandEditVm;
            if (id is null || id == 0)
            {
                brandEditVm = new BrandEditVm();
            }
            else
            {
                try
                {
                    Brand? brand = _BrandService.Get(filter: b => b.BrandId == id);
                    if (brand == null)
                    {
                        return NotFound();
                    }
                    brandEditVm = _mapper.Map<BrandEditVm>(brand);
                    return View(brandEditVm);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "An error occurred while retrieving the record");
                }
            }
            return View(brandEditVm);

        }

        [HttpPost]
        public IActionResult UpSert(BrandEditVm brandEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(brandEditVm);
            }

            if (_BrandService == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Dependencies are not configured correctly");
            }
            try
            {
                Brand brand = _mapper.Map<Brand>(brandEditVm);
                if (_BrandService.Exist(brand))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(brandEditVm);
                }
                _BrandService.Save(brand);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record");
                return View(brandEditVm);
            }
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Brand? brand = _BrandService?.Get(filter: b => b.BrandId == id);
            if (brand is null)
            {
                return NotFound();
            }
            try
            {
                if (_BrandService == null || _mapper == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       "Dependencies are not configured correctly");
                }

                if (_BrandService.ItsRelated(brand.BrandId))
                {
                    return Json(new { success = false, message = "Related Record...Delete Deny!!" });

                }
                _BrandService.Delete(brand);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete record!!!" }); ;

            }
        }


    }
}
