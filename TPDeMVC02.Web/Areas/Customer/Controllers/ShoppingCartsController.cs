using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;
using TPDeMVC02.Web.ViewModels.OrderHeaders;
using TPDeMVC02.Web.ViewModels.ShoppingCarts;

namespace TPDeMVC02.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartsServicio? _cartsServicio;
        private readonly ICountriesServicio? _countriesServicio;
        private readonly IStatesServicio? _statesServicio;
        private readonly ICitiesServicio? _citiesServicio;
        private readonly IApplicationUsersServicio? _applicationUsersServicio;
        private readonly IOrderHeadersServicio? _orderHeadersServicio;
        private readonly IMapper? _mapper;

        public ShoppingCartsController(IShoppingCartsServicio? cartsServicio,
            ICountriesServicio? countriesServicio, IStatesServicio? statesServicio,
            ICitiesServicio? citiesServicio,
            IApplicationUsersServicio? applicationUsersServicio, 
            IOrderHeadersServicio? orderHeadersServicio, IMapper? mapper)
        {
            _cartsServicio = cartsServicio ?? throw new ApplicationException("Dependencies not set");
            _countriesServicio = countriesServicio ?? throw new ApplicationException("Dependencies not set");
            _statesServicio = statesServicio ?? throw new ApplicationException("Dependencies not set");
            _citiesServicio = citiesServicio ?? throw new ApplicationException("Dependencies not set");
            _applicationUsersServicio = applicationUsersServicio 
                ?? throw new ApplicationException("Dependencies not set");
            _orderHeadersServicio = orderHeadersServicio ?? throw new ApplicationException("Dependencies not set");
            _mapper = mapper;
        }
        public IActionResult Index(string? returnUrl = null)
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity!;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var cartList = _cartsServicio!.GetAll(
                filter: c => c.ApplicationUserId == userId,
                propertiesNames: @"ShoeSize.shoe.Images,ShoeSize.size,ShoeColor.Color,ShoeColor.Shoe"
            )!.ToList();

            foreach (var cart in cartList)
            {
                cart.ShoeSize.shoe.Images = cart.ShoeSize.shoe.Images.Take(1).ToList();
            }

            var shoppingVm = new ShoppingCartListVm
            {
                ShoppingCarts = cartList,
                OrderHeader = new OrderHeaderEditVm()
                {
                    OrderTotal = CalculateTotal(cartList)
                }
            };

            ViewBag.ReturnUrl = returnUrl;
            return View(shoppingVm);
        }
        private decimal CalculateTotal(List<ShoppingCart> cartList)
        {
            var total = 0M;
            foreach (var item in cartList)
            {
                var basePrice = item.ShoeColor.Shoe.Price;
                var colorAdjustment = item.ShoeColor.PriceAdjustment;
                total += (item.Quantity == 1 ? (basePrice + colorAdjustment)
                    : (basePrice + colorAdjustment) * 0.9M) * item.Quantity;
            }
            return total;
        }

        public IActionResult Summary(string? returnUrl = null)
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity!;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var cartList = _cartsServicio!.GetAll(filter: c => c.ApplicationUserId == claims!.Value,
                           propertiesNames: "ShoeSize.shoe,ShoeSize.size,ShoeColor.Color,ShoeColor.Shoe")!.ToList();

            ShoppingCartListVm shoppingVm = new ShoppingCartListVm
            {
                ShoppingCarts = cartList,
                OrderHeader = new OrderHeaderEditVm()
                {
                    OrderTotal = CalculateTotal(cartList),
                    OrderDate = DateTime.Now,
                    ShippingDate = DateTime.Now.AddDays(3),
                    Countries = GetCountries(),
                    States = GetCountryStates(),
                    Cities = GetStateCities(),

                }
            };
            var user = _applicationUsersServicio!.Get(filter: u => u.Id == claims!.Value);
            shoppingVm.OrderHeader.ApplicationUserId = user!.Id;
            shoppingVm.OrderHeader.FirstName = user.FirstName;
            shoppingVm.OrderHeader.LastName = user.LastName;
            shoppingVm.OrderHeader.Address = user.Address;
            shoppingVm.OrderHeader.ZipCode = user.ZipCode;
            shoppingVm.OrderHeader.CountryId = user.CountryId;
            shoppingVm.OrderHeader.StateId = user.StateId;
            shoppingVm.OrderHeader.CityId = user.CityId;
            shoppingVm.OrderHeader.Phone = user.Phone;

            ViewBag.ReturnUrl = returnUrl;
            return View(shoppingVm);
        }
        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPOST(ShoppingCartListVm shoppingVm)
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity!;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var cartList = _cartsServicio!.GetAll(filter: c => c.ApplicationUserId == claims!.Value,
                            propertiesNames: "ShoeSize.shoe,ShoeSize.size,ShoeColor.Color,ShoeColor.Shoe")!.ToList();
            shoppingVm.OrderHeader!.OrderTotal = CalculateTotal(cartList);
            shoppingVm.OrderHeader.OrderDate = DateTime.Now;
            shoppingVm.OrderHeader.ShippingDate = DateTime.Now.AddDays(3);
            shoppingVm.OrderHeader.OrderDetails = _mapper!.Map<List<OrderDetail>>(cartList);
            shoppingVm.OrderHeader.Countries = GetCountries();
            shoppingVm.OrderHeader.States = GetCountryStates();
            shoppingVm.OrderHeader.Cities = GetStateCities();
            if (!ModelState.IsValid)
            {
                return View(shoppingVm);
            }
            OrderHeader orderHeader = _mapper.Map<OrderHeader>(shoppingVm.OrderHeader);
            try
            {
                _orderHeadersServicio!.Save(orderHeader);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(shoppingVm);
            }
            return RedirectToAction("OrderConfirmed", new { id = orderHeader.OrderHeaderId });
        }
        public IActionResult OrderConfirmed(int id)
        {
            return View(id);
        }
        public IActionResult OrderCancelled()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity!;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var cartList = _cartsServicio!.GetAll(filter: c => c.ApplicationUserId == claims!.Value,
                    propertiesNames: "ShoeSize")!.ToList();
            foreach (var item in cartList)
            {
                item.ShoeSize.StockInCarts -= item.Quantity;
                item.ShoeSize.AvailableStock += item.Quantity;
                _cartsServicio.Delete(item);
            }
            return View();
        }
        public IActionResult Plus(int id, string? returnUrl = null)
        {
            var cartInDb = _cartsServicio!.Get(filter: c => c.ShoppingCartId == id, propertiesNames: "ShoeSize");

            if (cartInDb!.ShoeSize.AvailableStock >= 1)
            {
                cartInDb.ShoeSize.StockInCarts += 1;
                cartInDb.ShoeSize.AvailableStock -= 1;
                cartInDb.Quantity += 1;
                _cartsServicio.Save(cartInDb);
                TempData["success"] = "added to cart";
            }
            else
            {
                TempData["error"] = "Not stock available!";
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public IActionResult Minus(int id, string? returnUrl = null)
        {
            var cartInDb = _cartsServicio!.Get(filter: c => c.ShoppingCartId == id, propertiesNames: "ShoeSize");
            cartInDb!.Quantity -= 1;
            cartInDb.ShoeSize.StockInCarts -= 1;
            cartInDb.ShoeSize.AvailableStock += 1;

            if (cartInDb.Quantity == 0)
            {
                _cartsServicio.Delete(cartInDb);
            }
            else
            {
                _cartsServicio.Save(cartInDb);

            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public IActionResult Remove(int id, string? returnUrl = null)
        {
            var cartInDb = _cartsServicio!.Get(filter: c => c.ShoppingCartId == id, propertiesNames: "ShoeSize");
            cartInDb!.ShoeSize.StockInCarts -= cartInDb.Quantity;
            cartInDb!.ShoeSize.AvailableStock = cartInDb.ShoeSize.QuantityInStock;
            _cartsServicio.Delete(cartInDb);
            return RedirectToAction("Index", new { returnUrl });
        }

        private List<SelectListItem> GetCountries()
        {
            return _countriesServicio!.GetAll(orderBy: o => o.OrderBy(c => c.CountryName))
                    .Select(c => new SelectListItem
                    {
                        Text = c.CountryName,
                        Value = c.CountryId.ToString()
                    }).ToList();
        }
        private List<SelectListItem> GetCountryStates(int? countryId = null)
        {
            IEnumerable<State>? states;
            if (countryId is null)
            {
                states = _statesServicio!.GetAll(orderBy: o => o.OrderBy(c => c.StateName));
            }
            else
            {
                states = _statesServicio!.GetAll(orderBy: o => o.OrderBy(c => c.StateName),
                         filter: s => s.CountryId == countryId);

            }
            return states.Select(c => new SelectListItem
            {
                Text = c.StateName,
                Value = c.StateId.ToString()
            }).ToList();

        }
        private List<SelectListItem> GetStateCities(int? stateId = null)
        {
            IEnumerable<City>? cities;
            if (stateId is null)
            {
                cities = _citiesServicio!.GetAll(orderBy: o => o.OrderBy(c => c.CityName));
            }
            else
            {
                cities = _citiesServicio!.GetAll(orderBy: o => o.OrderBy(c => c.CityName),
                         filter: c => c.StateId == stateId);

            }
            return cities
                .Select(c => new SelectListItem
                {
                    Text = c.CityName,
                    Value = c.CityId.ToString()
                }).ToList();

        }
        #region API CALLS
        [HttpGet]
        public JsonResult GetCartItemCount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Json(0);

            var itemCount = _cartsServicio!.GetAll(filter: sc => sc.ApplicationUserId == userId)!
                                                     .Sum(sc => sc.Quantity);
            return Json(itemCount);
        }
        #endregion
    }
}
