using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;
using TPDeMVC02.Web.ViewModels.Shoes;
using X.PagedList.Extensions;

namespace TPDeMVC02.Web.Controllers
{
    public class ShoesController : Controller
    {
        private readonly IShoeServicio? _shoeServicio;
        private readonly IBrandService? _brandService;
        private readonly ISportServicio? _sportServicio;
        private readonly IGenreServicio? _genreServicio;
        private readonly IColorServicio? _colorServicio;
        private readonly IMapper? _mapper;

        public ShoesController(IShoeServicio? shoeServicio, IBrandService brandService, ISportServicio sportServicio,
            IGenreServicio genreServicio, IColorServicio colorServicio,
            IMapper? mapper)
        {
            _shoeServicio = shoeServicio ?? throw new ApplicationException("Dependencies not set");
            _brandService = brandService ?? throw new ApplicationException("Dependencies not set");
            _sportServicio = sportServicio ?? throw new ApplicationException("Dependencies not set");
            _genreServicio = genreServicio ?? throw new ApplicationException("Dependencies not set");
            _colorServicio = colorServicio ?? throw new ApplicationException("Dependencies not set");
            _mapper = mapper;
        }
		public IActionResult Index(int? page, int? filterBrandId,int? filterColorId,
            int pageSize = 10, bool viewAll = false,string orderBy= "Description")
		{
			int PageNumber = page ?? 1;
			ViewBag.currentPageSize = pageSize;
			ViewBag.currentOrderBy = orderBy;

			var Brands = _brandService!.GetAll(orderBy: o => o.OrderBy(b => b.BrandName))!.ToList();
			var colors = _colorServicio!.GetAll(orderBy: o => o.OrderBy(c => c.ColorName))!.ToList();
			IEnumerable<Shoe>? shoes;

			if (viewAll || (filterBrandId is null && filterColorId is null))
			{
				shoes = _shoeServicio?.GetAll(orderBy: o => o.OrderBy(s => s.Description),
					propertiesNames: "Brand,Sport,Genre,Color");
			}
			else
			{
				if (filterBrandId.HasValue)
				{
					shoes = _shoeServicio!.GetAll(orderBy: o => o.OrderBy(s => s.Description),
						filter: b => b.BrandId == filterBrandId.Value,
						propertiesNames: "Brand,Sport,Genre,Color");
				}
				else if (filterColorId.HasValue)
				{
					shoes = _shoeServicio!.GetAll(orderBy: o => o.OrderBy(s => s.Description),
					filter: s => s.ColorId == filterColorId.Value,
					propertiesNames: "Brand,Sport,Genre,Color");
				}
				else
				{
					shoes = _shoeServicio?.GetAll(orderBy: o => o.OrderBy(s => s.Description),
						propertiesNames: "Brand,Sport,Genre,Color");
				}
			}

			var shoesVm = _mapper?.Map<List<ShoeListVm>>(shoes);
		
			if (orderBy=="Brand")
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
			if (orderBy == "Color")
			{
				shoesVm = shoesVm!.OrderBy(c=> c.Color).ToList();
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
				Colors = colors.Select(c => new SelectListItem
				{
					Text = c.ColorName,
					Value = c.ColorId.ToString()
				}).ToList()
			};
			return View(ShoeFilterVm);
		}

		public IActionResult UpSert(int? id)
        {
            ShoeEditVm shoeEditVm;
            if (id == null || id == 0)
            {
                shoeEditVm = new ShoeEditVm();
                shoeEditVm.Brands = GetBrands();
                shoeEditVm.Genres = GetGenres();
                shoeEditVm.Sports = GetSports();
                shoeEditVm.Colors = GetColors();
            }
            else
            {
                try
                {
                    Shoe? shoe = _shoeServicio!.Get(filter: s => s.ShoeId == id, propertiesNames: "Brand,Sport,Genre,Color");
                    if (shoe == null)
                    {
                        return NotFound();
                    }
                    shoeEditVm = _mapper!.Map<ShoeEditVm>(shoe);
                    shoeEditVm.Brands = GetBrands();
                    shoeEditVm.Genres = GetGenres();
                    shoeEditVm.Sports = GetSports();
                    shoeEditVm.Colors = GetColors();
                    return View(shoeEditVm);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record");
                    throw;
                }
            }
            return View(shoeEditVm);
        }

        private List<SelectListItem> GetColors()
        {
            return _colorServicio!.GetAll(orderBy: o => o.OrderBy(c => c.ColorName))!
           .Select(c => new SelectListItem
           {
               Text = c.ColorName,
               Value = c.ColorId.ToString()

           }).ToList();
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
                shoeEditVm.Colors = GetColors();
                return View(shoeEditVm);
            }
            if (_shoeServicio == null || _mapper == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Dependencies are not configured correctly");
            }
            try
            {
                Shoe shoe = _mapper!.Map<Shoe>(shoeEditVm);
                if (_shoeServicio!.Exist(shoe))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    shoeEditVm.Brands = GetBrands();
                    shoeEditVm.Genres = GetGenres();
                    shoeEditVm.Sports = GetSports();
                    shoeEditVm.Colors = GetColors();
                    return View(shoeEditVm);
                }
                _shoeServicio.Save(shoe);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while  record");
                shoeEditVm.Brands = GetBrands();
                shoeEditVm.Genres = GetGenres();
                shoeEditVm.Sports = GetSports();
                shoeEditVm.Colors = GetColors();
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
                    return Json(new { success = false, message = "Related Record...Delete Deny!!" });

                }
                _shoeServicio.Delete(shoe);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Couldn't delete record!!!" }); ;

            }
        }
    }
}
