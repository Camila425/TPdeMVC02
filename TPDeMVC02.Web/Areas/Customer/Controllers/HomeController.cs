using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
        private readonly IMapper _mapper;
        private readonly int pageSize = 8;
        public HomeController(IShoeServicio shoeServicio, IMapper mapper)
        {
            _shoeServicio = shoeServicio;
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
            return View(shoesVm.ToPagedList(currentPage,pageSize));
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
