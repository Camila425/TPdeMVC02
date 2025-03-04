using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Entidades.Dtos;
using TPdeEFCore01.Servicios.Interfaces;
using TPDeMVC02.Web.ViewModels.Shoes;
using X.PagedList.Extensions;

namespace TPDeMVC02.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ShoesController : Controller
    {
        private readonly IShoeServicio? _shoeServicio;
        private readonly IBrandServicio? _brandService;
        private readonly ISportServicio? _sportServicio;
        private readonly IGenreServicio? _genreServicio;
        private readonly IColorServicio? _colorServicio;
        private readonly IShoeSizesServicio? _shoeSizesServicio;
        private readonly ISizeServicio? _sizeServicio;
        private readonly IShoeImagesServicio? _shoeImagesServicio;
		private readonly IShoeColorsServicio? _shoeColorsServicio;

		private readonly IMapper? _mapper;
        private readonly IWebHostEnvironment? _webHostEnvironment;


        public ShoesController(IShoeServicio? shoeServicio, IBrandServicio brandService, ISportServicio sportServicio,
            IGenreServicio genreServicio, IColorServicio colorServicio,
            IShoeSizesServicio? shoeSizesServicio,
            ISizeServicio? sizeServicio,
            IShoeImagesServicio? shoeImagesServicio,
            IWebHostEnvironment webHostEnvironment,
			IShoeColorsServicio? shoeColorsServicio,
			IMapper? mapper)
        {
            _shoeServicio = shoeServicio ?? throw new ApplicationException("Dependencies not set");
            _brandService = brandService ?? throw new ApplicationException("Dependencies not set");
            _sportServicio = sportServicio ?? throw new ApplicationException("Dependencies not set");
            _genreServicio = genreServicio ?? throw new ApplicationException("Dependencies not set");
            _colorServicio = colorServicio ?? throw new ApplicationException("Dependencies not set");
            _shoeSizesServicio = shoeSizesServicio ?? throw new ApplicationException("Dependencies not set");
            _sizeServicio = sizeServicio ?? throw new ApplicationException("Dependencies not set");
            _shoeImagesServicio = shoeImagesServicio ?? throw new ApplicationException("Dependencies not set");
			_shoeColorsServicio = shoeColorsServicio ?? throw new ApplicationException("Dependencies not set");
			_webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        public IActionResult Index(int? page, int? filterBrandId,int pageSize = 10, 
            bool viewAll = false, string orderBy = "Description")
        {
            int PageNumber = page ?? 1;
            ViewBag.currentPageSize = pageSize;
            ViewBag.currentOrderBy = orderBy;

            var Brands = _brandService!.GetAll(orderBy: o => o.OrderBy(b => b.BrandName))!.ToList();
            IEnumerable<Shoe>? shoes;
            if (viewAll || filterBrandId is null)
            {
                shoes = _shoeServicio?.GetAll(orderBy: o => o.OrderBy(s => s.Description),
                    propertiesNames: "Brand,Sport,Genre");
            }
            else
            {
                if (filterBrandId.HasValue)
                {
                    shoes = _shoeServicio!.GetAll(orderBy: o => o.OrderBy(s => s.Description),
                        filter: b => b.BrandId == filterBrandId.Value,
                        propertiesNames: "Brand,Sport,Genre");
                }
                else
                {
                    shoes = _shoeServicio?.GetAll(orderBy: o => o.OrderBy(s => s.Description),
                        propertiesNames: "Brand,Sport,Genre");
                }
            }

            var shoesVm = _mapper?.Map<List<ShoeListVm>>(shoes);

            if (orderBy == "Brand")
            {
                shoesVm = shoesVm!.OrderBy(b => b.Brand).ToList();
            }
            if (orderBy == "Genre")
            {
                shoesVm = shoesVm!.OrderBy(g => g.Genre).ToList();
            }
            if (orderBy == "Sport")
            {
                shoesVm = shoesVm!.OrderBy(s => s.Sport).ToList();
            }
            if (orderBy == "Model")
            {
                shoesVm = shoesVm!.OrderBy(s => s.Model).ToList();
            }

            var ShoeFilterVm = new ShoeFilterVm
            {
                Shoes = shoesVm!.ToPagedList(PageNumber, pageSize),
                Brands = Brands.Select(b => new SelectListItem
                {
                    Text = b.BrandName,
                    Value = b.BrandId.ToString()
                }).ToList(),
            };
            return View(ShoeFilterVm);
        }

        public IActionResult UpSert(int? id, string? returnUrl = null)
        {
            ShoeEditVm shoeEditVm;
            if (id == null || id == 0)
            {
                shoeEditVm = new ShoeEditVm();
                shoeEditVm.Brands = GetBrands();
                shoeEditVm.Genres = GetGenres();
                shoeEditVm.Sports = GetSports();
            }
            else
            {
                try
                {
                    string? wwwWebRoot = _webHostEnvironment!.WebRootPath;
                    Shoe? shoe = _shoeServicio!.Get(filter: s => s.ShoeId == id,
                        propertiesNames: "Brand,Sport,Genre,Images");
                    if (shoe == null)
                    {
                        return NotFound();
                    }
                    if (shoe.Images != null && shoe.Images.Any())
                    {
                        foreach (var image in shoe.Images)
                        {
                            if (!string.IsNullOrEmpty(image.ImageUrl))
                            {
                                var filePath = Path.Combine(wwwWebRoot, image.ImageUrl.TrimStart('/'));
                                if (System.IO.File.Exists(filePath))
                                {
                                    ViewData["ImageExist"] = true;
                                    break;
                                }
                            }
                        }
                        if (ViewData["ImageExist"] == null)
                        {
                            ViewData["ImageExist"] = false;
                        }
                    }
                    else
                    {
                        ViewData["ImageExist"] = false;
                    }
                    shoeEditVm = _mapper!.Map<ShoeEditVm>(shoe);
                    shoeEditVm.Brands = GetBrands();
                    shoeEditVm.Genres = GetGenres();
                    shoeEditVm.Sports = GetSports();
                    shoeEditVm.ReturnUrl = returnUrl;
                    return View(shoeEditVm);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "An error occurred while retrieving the record");
                }
            }
            return View(shoeEditVm);
        }
        private List<SelectListItem> GetSports()
        {
            return _sportServicio!.GetAll(orderBy: o => o.OrderBy(s => s.SportName))!
           .Select(s => new SelectListItem
           {
               Text = s.SportName,
               Value = s.SportId.ToString()

           }).ToList();
        }

        private List<SelectListItem> GetGenres()
        {
            return _genreServicio!.GetAll(orderBy: o => o.OrderBy(g => g.GenreName))!
           .Select(g => new SelectListItem
           {
               Text = g.GenreName,
               Value = g.GenreId.ToString()

           }).ToList();
        }

        private List<SelectListItem> GetBrands()
        {
            return _brandService!.GetAll(orderBy: o => o.OrderBy(b => b.BrandName))!
            .Select(b => new SelectListItem
            {
                Text = b.BrandName,
                Value = b.BrandId.ToString()

            }).ToList();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(ShoeEditVm shoeEditVm)
        {
            if (!ModelState.IsValid)
            {
                shoeEditVm.Brands = GetBrands();
                shoeEditVm.Genres = GetGenres();
                shoeEditVm.Sports = GetSports();
                return View(shoeEditVm);
            }

            if (_shoeServicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Dependencies are not configured correctly");
            }

            try
            {
                string? wwwWebRoot = _webHostEnvironment!.WebRootPath;
                Shoe shoe = _mapper.Map<Shoe>(shoeEditVm);

                _shoeServicio.SaveShoeWithImages(shoe, shoeEditVm.ImageFiles, wwwWebRoot);

                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                shoeEditVm.Brands = GetBrands();
                shoeEditVm.Genres = GetGenres();
                shoeEditVm.Sports = GetSports();
                return View(shoeEditVm);
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
            Shoe? shoe = _shoeServicio?.Get(filter: s => s.ShoeId == id);
            if (shoe is null)
            {
                return NotFound();
            }
            try
            {
                if (_shoeServicio == null || _mapper == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       "Dependencies are not configured correctly");
                }

                if (_shoeServicio.ItsRelated(shoe.ShoeId))
                {
                    return Json(new { success = false, message = "Related Record.   Delete Deny!!" });

                }
                _shoeServicio.Delete(shoe);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete record!!" }); ;

            }
        }

        public IActionResult AssignSizes(int? id)
        {
            ShoeAssignSizesVm shoeVm;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    Shoe? shoe = _shoeServicio?.Get(filter: s => s.ShoeId == id);
                    if (shoe == null)
                    {
                        return NotFound();
                    }
                    shoeVm = _mapper!.Map<ShoeAssignSizesVm>(shoe);
                    shoeVm.AvailableSizes = GetSizesWithStocks(shoe.ShoeId);

                    var assignedSizeIds = shoeVm.AvailableSizes.Select(s => s.SizeId).ToList();
                    shoeVm.AllSizes = GetAllAvailableAndNotAssignedSizes(shoeVm, assignedSizeIds);

                }
                catch (Exception)
                {
                    throw;
                }
                return View(shoeVm);
            }
        }

        private IEnumerable<SelectListItem> GetAllAvailableAndNotAssignedSizes
            (ShoeAssignSizesVm shoeVm, List<int> assignedSizeIds)
        {
            return _sizeServicio!.GetAll(filter: S => !assignedSizeIds.Contains(S.SizeId))!
                .Select(s => new SelectListItem
                {
                    Value = s.SizeId.ToString(),
                    Text = s.SizeNumber.ToString()
                }).ToList();
        }

        public List<ShoeSizeVm> GetSizesWithStocks(int shoeId)
        {
            var AssignedSizes = _shoeSizesServicio!.GetAll(filter: ss => ss.ShoeId == shoeId, propertiesNames: "size")!
                .Select(ss => new ShoeSizeVm
                {
                    SizeId = ss.SizeId,
                    SizeNumber = ss.size.SizeNumber,
                    Stock = ss.QuantityInStock,
                    StockInCarts=ss.StockInCarts,
                    AvailableStock=ss.AvailableStock,
                    IsSelected = true,
                }).ToList();
            return AssignedSizes;
        }

        [HttpPost]
        public IActionResult AssignSizes(ShoeAssignSizesVm shoeAssignSizesVm)
        {
            if (ModelState.IsValid)
            {
                var ShoeSizeDto = _mapper!.Map<ShoeSizeDto>(shoeAssignSizesVm);

                _shoeSizesServicio!.AssignSizesAndStockToShoe(ShoeSizeDto);

                return RedirectToAction("AssignSizes", new { id = shoeAssignSizesVm.ShoeId });
            }
            shoeAssignSizesVm.AvailableSizes = GetSizesWithStocks(shoeAssignSizesVm.ShoeId);
            return View(shoeAssignSizesVm);
        }

        public ActionResult EditStock(int? ShoeId, int? sizeId)
        {
            EditStockShoeSize editStockShoeSize;
            if (ShoeId == null || sizeId == 0)
            {
                editStockShoeSize = new EditStockShoeSize();
            }
            var shoesize = _shoeSizesServicio!.GetAll(filter: ss => ss.ShoeId == ShoeId && ss.SizeId == sizeId,
            propertiesNames: "shoe,size")!
                 .Select(ss => new EditStockShoeSize
                 {
                     SizeId = ss.SizeId,
                     ShoeId = ss.ShoeId,
                     StockActual = ss.QuantityInStock,
                 }).FirstOrDefault();
            if (shoesize == null)
            {
                TempData["ERROR"] = "Shoe Size Not Found";
                return RedirectToAction("AssignSizes");
            }
            editStockShoeSize = _mapper!.Map<EditStockShoeSize>(shoesize);

            return View(editStockShoeSize);
        }

        [HttpPost]
        public ActionResult EditStock(EditStockShoeSize editStockShoeSize)
        {
            if (ModelState.IsValid)
            {
                var shoeSize = _shoeSizesServicio!.GetAll(
                    filter: ss => ss.ShoeId == editStockShoeSize.ShoeId && ss.SizeId == editStockShoeSize.SizeId,
                    propertiesNames: "shoe,size")!
                    .FirstOrDefault();

                if (shoeSize == null)
                {
                    TempData["ERROR"] = "Shoe Size Not Found";
                    return RedirectToAction("AssignSizes");
                }
                shoeSize.QuantityInStock = editStockShoeSize.StockNuevo;

                shoeSize.AvailableStock = shoeSize.QuantityInStock - shoeSize.StockInCarts;

                _shoeSizesServicio.Save(shoeSize);

                TempData["success"] = "Record successfully added/edited";
            }
            return View(editStockShoeSize);
        }
		public IActionResult AssignColors(int? id)
		{
			ShoeAssignColorsVm shoeVm;

			if (id == null || id == 0)
			{
				return NotFound();
			}
			else
			{
				try
				{
					Shoe? shoe = _shoeServicio?.Get(filter: b => b.ShoeId == id);
					if (shoe == null)
					{
						return NotFound();
					}

					shoeVm = _mapper!.Map<ShoeAssignColorsVm>(shoe);

					shoeVm.AvailableColors = GetColorsWithPrices(shoe.ShoeId);

					var assignedColorIds = shoeVm.AvailableColors.Select(c => c.ColorId).ToList();
					shoeVm.AllColors = GetAllAvailableAndNotAssignedColours(shoeVm, assignedColorIds);
				}
				catch (Exception)
				{
					return StatusCode(StatusCodes.Status500InternalServerError,
                        "An error occurred while retrieving the record.");
				}
			}

			return View(shoeVm);
		}
		private IEnumerable<SelectListItem> GetAllAvailableAndNotAssignedColours
		   (ShoeAssignColorsVm shoeVm, List<int> assignedColourIds)
		{
			return _colorServicio!.GetAll(filter: c => !assignedColourIds.Contains(c.ColorId))!
				.Select(c => new SelectListItem
				{
					Value = c.ColorId.ToString(),
					Text = c.ColorName
				}).ToList();
		}
		public List<ShoeColorVm> GetColorsWithPrices(int shoeId)
		{
			var assignedColours = _shoeColorsServicio!.
                GetAll(filter: sc => sc.ShoeId == shoeId, propertiesNames: "Color")!
				.Select(sc => new ShoeColorVm
				{
					ColorId = sc.ColorId,
					ColorName = sc.Color.ColorName,
					Price = sc.PriceAdjustment,
					IsSelected = true 
				}).ToList();

			return assignedColours;
		}
		[HttpPost]
		public IActionResult AssignColors(ShoeAssignColorsVm shoeColorVm)
		{
			if (ModelState.IsValid)
			{
				var shoeColorDto = _mapper!.Map<ShoeColorDto>(shoeColorVm);


				_shoeColorsServicio!.AssignColorsAndPricesToShoe(shoeColorDto);
				return RedirectToAction("AssignColors");
			}

			shoeColorVm.AvailableColors = GetColorsWithPrices(shoeColorVm.ShoeId);
			return View(shoeColorVm);
		}

	}
}
