using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;
using TPDeMVC02.Web.ViewModels.Brands;
using TPDeMVC02.Web.ViewModels.Shoes;
using X.PagedList.Extensions;

namespace TPDeMVC02.Web.Controllers
{
	public class BrandsController : Controller
	{
		private readonly IBrandService? _BrandService;
		private readonly IShoeServicio? _shoeServicio;

		private readonly IMapper? _mapper;
		private readonly IWebHostEnvironment? _webHostEnvironment;

		public BrandsController(IBrandService? BrandService, IShoeServicio? shoeServicio,
		  IWebHostEnvironment webHostEnvironment,
			IMapper mapper)
		{
			_BrandService = BrandService ?? throw new ApplicationException("dependencies not set");
			_shoeServicio = shoeServicio ?? throw new ApplicationException("dependencies not set"); ;
			_webHostEnvironment = webHostEnvironment;
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
			foreach (var brand in brandsVm!)
			{
				brand.ShoesQuantity = GetShoeQuantity(brand.BrandId);
			}
			return View(brandsVm);

		}

		private int GetShoeQuantity(int brandId)
		{
			return _shoeServicio!.GetAll(filter: s => s.BrandId == brandId)!.Count();
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
					string? wwwWebRoot = _webHostEnvironment!.WebRootPath;
					Brand? brand = _BrandService.Get(filter: b => b.BrandId == id);
					if (brand == null)
					{
						return NotFound();
					}
					var filePath = Path.Combine(wwwWebRoot, brand.ImageUrl!.TrimStart('/'));
					ViewData["ImageExist"] = System.IO.File.Exists(filePath);
					brandEditVm = _mapper.Map<BrandEditVm>(brand);
					return View(brandEditVm);
				}
				catch (Exception)
				{
					return StatusCode(StatusCodes.Status500InternalServerError,
						"An error occurred while record");
				}
			}
			return View(brandEditVm);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
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
				string? wwwWebRoot = _webHostEnvironment!.WebRootPath;
				Brand brand = _mapper.Map<Brand>(brandEditVm);

				if (_BrandService.Exist(brand))
				{
					ModelState.AddModelError(string.Empty, "Record already exist");
					return View(brandEditVm);
				}

				if (brandEditVm.ImageFile != null)
				{
					var permittedExtensions = new string[] { ".png", ".jpg", ".jpeg", ".gif" };
					var fileExtension = Path.GetExtension(brandEditVm.ImageFile.FileName);
					if (!permittedExtensions.Contains(fileExtension))
					{
						ModelState.AddModelError(string.Empty, "File not allowed");
						return View(brandEditVm);
					}
					if (brandEditVm.ImageUrl != null)
					{
						string oldFilePath = Path.Combine(wwwWebRoot, brand.ImageUrl!.TrimStart('/'));
						if (System.IO.File.Exists(oldFilePath))
						{
							System.IO.File.Delete(oldFilePath);
						}
					}
					string fileName = $"{Guid.NewGuid()}{Path.GetExtension(brandEditVm.ImageFile.FileName)}";
					string pathName = Path.Combine(wwwWebRoot, "images", fileName);

					using (var fileStream = new FileStream(pathName, FileMode.Create))
					{
						brandEditVm.ImageFile.CopyTo(fileStream);
					}
					brand.ImageUrl = $"/images/{fileName}";
				}
				_BrandService.Save(brand);
				TempData["success"] = "Record successfully added/edited";
				return RedirectToAction("Index");
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "An error occurred while record");
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
				string? wwwWebRoot = _webHostEnvironment!.WebRootPath;

				string oldFilePath = Path.Combine(wwwWebRoot, brand.ImageUrl!.TrimStart('/'));
				if (System.IO.File.Exists(oldFilePath))
				{
					System.IO.File.Delete(oldFilePath);
				}
				return Json(new { success = true, message = "Record successfully deleted" });
			}
			catch (Exception)
			{
				return Json(new { success = false, message = "Couldn't delete record!!!" }); ;

			}
		}
		public IActionResult Details(int? id, int? page)
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
			var currentPage = page ?? 1;
			int pageSize = 10;

			BrandDetailsVm brandDetails = _mapper!.Map<BrandDetailsVm>(brand);
			brandDetails.ShoesQuantity = GetShoeQuantity(brandDetails.BrandId);
			var shoes = _shoeServicio!.GetAll(
				orderBy: o => o.OrderBy(s => s.Description),
				filter: b => b.BrandId == brandDetails.BrandId,
				propertiesNames: "Brand,Sport,Genre,Color");
			brandDetails.ShoesListVm = _mapper!.Map<List<ShoeListVm>>(shoes).ToPagedList(currentPage, pageSize);

			return View(brandDetails);
		}

	}
}
