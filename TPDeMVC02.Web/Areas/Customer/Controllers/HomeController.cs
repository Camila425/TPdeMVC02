using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;
using TPDeMVC02.Web.Models;
using TPDeMVC02.Web.ViewModels.Shoes;
using TPDeMVC02.Web.ViewModels.ShoppingCarts;
using X.PagedList.Extensions;

namespace TPDeMVC02.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IShoeServicio _shoeServicio;
        private readonly IColorServicio _colorServicio;
        private readonly ISizeServicio _sizeServicio;
        private readonly IShoeSizesServicio _shoeSizesServicio;
        private readonly IShoeColorsServicio _shoeColorsServicio;

        private readonly IShoppingCartsServicio _shoppingCartsServicio;


        private readonly IShoeImagesServicio _shoeImagesServicio;
        private readonly IMapper _mapper;
        private readonly int pageSize = 8;
        public HomeController(IShoeServicio shoeServicio, IColorServicio colorServicio,
            ISizeServicio sizeServicio,
            IShoeSizesServicio shoeSizesServicio,
            IShoeImagesServicio shoeImagesServicio,
            IShoppingCartsServicio shoppingCartsServicio,
            IShoeColorsServicio shoeColorsServicio,
            IMapper mapper)
        {
            _shoeServicio = shoeServicio;
            _colorServicio = colorServicio;
            _sizeServicio = sizeServicio;
            _shoeSizesServicio = shoeSizesServicio;
            _shoeImagesServicio = shoeImagesServicio;
            _shoppingCartsServicio = shoppingCartsServicio;
            _shoeColorsServicio = shoeColorsServicio;
            _mapper = mapper;
        }

        public IActionResult Hero()
        {
            return View();
        }

        public IActionResult Index(int? page)
        {
            var currentPage = page ?? 1;
            var shoeImage = _shoeImagesServicio!.GetAll(
                orderBy: o => o.OrderBy(s => s.ShoeId),
                propertiesNames: "shoe,shoe.shoeSizes,shoe.ShoeColors");

            var groupByShoeImages = shoeImage!
                .GroupBy(img => img.ShoeId)
                .Select(group => group.First())
                .ToList();

            var shoesVm = _mapper!.Map<List<ShoeHomeIndexVm>>(groupByShoeImages);
            ViewBag.CurrentPage = currentPage;
            return View(shoesVm.ToPagedList(currentPage, pageSize));
        }

        public IActionResult Details(int? id, int? sizeId, int? ColorId, string? returnUrl = null)
        {
            if (id == null || sizeId == null)
            {
                return NotFound();
            }

            var shoesize = _shoeSizesServicio!.Get(
                filter: ss => ss.ShoeId == id && ss.SizeId == sizeId,
                propertiesNames: "shoe.Brand,size,shoe.Images,shoe");

            var shoecolor = _shoeColorsServicio!.Get(filter: sc => sc.ShoeId == id && sc.ColorId == ColorId,
              propertiesNames: "Color");

            if (shoesize == null)
            {
                return NotFound();
            }

            ShoeHomeDetailsVm shoeVm = _mapper.Map<ShoeHomeDetailsVm>(shoesize);

            shoeVm.ListColor = GetColors(shoesize.shoe);
            shoeVm.ListSizewithStock = GetSizes(shoesize.shoe);
            shoeVm.SizeStock = shoesize.shoe.shoeSizes
                .ToDictionary(ss => ss.SizeId, ss => ss.QuantityInStock);

            ShoppingCartDetailVm shoppingVm = new ShoppingCartDetailVm
            {
                ShoeSizeId = shoesize.ShoeSizeId,
                ShoeId = id.Value,
                SizeId = sizeId.Value,
                ColorId = shoecolor!.ColorId,
                shoeColor = shoecolor,
                shoeSize = shoesize,
                Quantity = 1,
                shoeHomeDetailsVm = shoeVm
            };

            ViewBag.ReturnUrl = returnUrl;

            return View(shoppingVm);
        }


        [HttpPost]
        [Authorize(Roles = "Customer")]
        public IActionResult Details(ShoppingCartDetailVm shoppingVm, string? returnUrl = null)
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity!;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            shoppingVm.ApplicationUserId = userId;

            ShoppingCart shoppingCart = _mapper!.Map<ShoppingCart>(shoppingVm);
            var carInDb = _shoppingCartsServicio?.Get(filter: c => c.ShoeSizeId == shoppingCart!.ShoeSizeId &&
                                                 c.ShoeColorId == shoppingCart.ShoeColorId &&
                                                 c.ApplicationUserId == shoppingCart.ApplicationUserId);
            var shoesize = _shoeSizesServicio?.Get(
                    filter: ss => ss.ShoeId == shoppingVm.ShoeId && ss.SizeId == shoppingVm.SizeId)!;

            var shoeColor = _shoeColorsServicio?.Get(
            filter: sc => sc.ShoeId == shoppingVm.ShoeId && sc.ColorId == shoppingVm.ColorId);

            if (shoeColor != null)
            {
                shoppingCart.ShoeColorId = shoeColor.ShoeColorId;
                shoppingCart.ShoeColor = shoeColor;
            }

            if (shoesize!.AvailableStock >= shoppingCart.Quantity)
            {
                if (carInDb == null)
                {
                    shoesize.StockInCarts += shoppingCart.Quantity;
                    shoesize.AvailableStock -= shoppingCart.Quantity;
                    shoppingCart.ShoeSize = shoesize;
                    _shoppingCartsServicio!.Save(shoppingCart);
                }
                else
                {
                    shoesize.StockInCarts += shoppingCart.Quantity;
                    carInDb.Quantity += shoppingCart.Quantity;
                    shoesize.AvailableStock -= shoppingCart.Quantity;
                    shoppingCart.ShoeSize = shoesize;
                    carInDb.ShoeColorId = shoeColor!.ShoeColorId;
                    carInDb.ShoeColor = shoeColor;
                    _shoppingCartsServicio!.Save(carInDb);
                }
                TempData["success"] = "ShoeSize successfully added to shopping cart";
            }
            else
            {
                TempData["error"] = "out of stock";
            }
            return RedirectToAction("Index");
        }


        private List<ShoeSizeVm> GetSizes(Shoe shoe)
        {
            var shoeAssignedSizes = _shoeServicio.GetAssignedSizeForShoe(shoe);

            var assignedSizes = _shoeSizesServicio.GetAll(
                filter: ss => ss.ShoeId == shoe.ShoeId,
                propertiesNames: "size");
            var result = assignedSizes!
                .Where(ss => shoeAssignedSizes.Contains(ss.SizeId))
                .Select(ss => new ShoeSizeVm
                {
                    SizeId = ss.SizeId,
                    SizeNumber = ss.size.SizeNumber,
                    Stock = ss.QuantityInStock,
                    StockInCarts = ss.StockInCarts,
                    AvailableStock = ss.AvailableStock,
                    IsSelected = true
                })
                .ToList();

            return result;
        }
        private List<ShoeColor> GetColors(Shoe shoe)
        {
            return _shoeColorsServicio.GetAll(
                filter: sc => sc.ShoeId == shoe.ShoeId,
                propertiesNames: "Color"
            )?.ToList() ?? new List<ShoeColor>();
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

        [HttpGet]
        public IActionResult GetPriceByColor(int shoeId, int colorId)
        {
            var price = _shoeColorsServicio.GetPriceByColor(shoeId, colorId);

            if (price <= 0)
            {
                return Json(new { success = false, message = "No se encontro el precio" });
            }

            return Json(new { success = true, price = price });
        }
    }
}
