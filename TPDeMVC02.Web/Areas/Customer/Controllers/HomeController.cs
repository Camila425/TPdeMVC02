using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;
using TPDeMVC02.Web.Models;
using TPDeMVC02.Web.ViewModels.Shoes;
using X.PagedList.Extensions;

namespace TPDeMVC02.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IShoeServicio _shoeServicio;
        private readonly IColorServicio _colorServicio;
        private readonly ISizeServicio _sizeServicio;
        private readonly IMapper _mapper;
        private readonly int pageSize = 8;
        public HomeController(IShoeServicio shoeServicio, IColorServicio colorServicio,
            ISizeServicio sizeServicio, IMapper mapper)
        {
            _shoeServicio = shoeServicio;
            _colorServicio = colorServicio;
            _sizeServicio = sizeServicio;
            _mapper = mapper;
        }

        public IActionResult Hero()
        {
            return View();
        }

        public IActionResult Index(int? page)
        {
            var currentPage = page ?? 1;
            var Shoes = _shoeServicio!.GetAll(orderBy: o => o.OrderBy(s => s.Model),
                propertiesNames: "Brand");
            var shoesVm = _mapper!.Map<List<ShoeHomeIndexVm>>(Shoes);
            return View(shoesVm.ToPagedList(currentPage, pageSize));
        }
        public IActionResult Details(int? id, string? returnUrl = null)
        {
            if (id == null || id.Value == 0)
            {
                return NotFound();
            }
            Shoe? shoe = _shoeServicio!.Get(filter: s => s.ShoeId == id,
                propertiesNames: "Brand");
            if (shoe is null)
            {
                return NotFound();
            }
            ShoeHomeDetailsVm shoeVm = _mapper!.Map<ShoeHomeDetailsVm>(shoe);
            shoeVm.ListColor = GetColors();
            shoeVm.ListSize = GetSizes(shoe);
            ViewBag.ReturnUrl = returnUrl;
            return View(shoeVm);
        }

        private List<SelectListItem>? GetSizes(Shoe shoe)
        {
            var Shoeassignedsize = _shoeServicio.GetAssignedSizeForShoe(shoe);


            return _sizeServicio!.GetAll(orderBy: o => o.OrderBy(s => s.SizeNumber))!
                      .Where(s => Shoeassignedsize.Contains(s.SizeId))
                      .Select(s => new SelectListItem
                      {
                          Text = s.SizeNumber.ToString(),
                          Value = s.SizeId.ToString()
                      }).ToList();
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
