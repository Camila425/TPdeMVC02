using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index(int? page, string? searchTerm = null, bool viewAll = false, int pageSize = 10)
        {
            int PageNumber = page ?? 1;
            ViewBag.currentPageSize = pageSize;
            IEnumerable<Shoe>? shoes;
            if (!viewAll)
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    shoes = _shoeServicio?.GetAll(orderBy: o => o.OrderBy(s => s.Description),
                        propertiesNames: "Brand,Sport,Genre,Color",
                        filter: s => s.Description.Contains(searchTerm) || s.Model!.Contains(searchTerm));
                    ViewBag.currentSearchTerm = searchTerm;

                }
                else
                {
                    shoes = _shoeServicio?.GetAll(orderBy: o => o.OrderBy(s => s.Description),
                        propertiesNames: "Brand,Sport,Genre,Color");
                }
            }
            else
            {
                shoes = _shoeServicio?.GetAll(orderBy: o => o.OrderBy(s => s.Description),
                    propertiesNames: "Brand,Sport,Genre,Color");
            }
            var shoesVm = _mapper?.Map<List<ShoeListVm>>(shoes).ToPagedList(PageNumber, pageSize);
            return View(shoesVm);

        }

    }
}
