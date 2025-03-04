using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TPdeEFCore01.Servicios.Interfaces;

namespace TPDeMVC02.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Admin, Customer")]
    public class OrdersController : Controller
    {
        private readonly IOrderHeadersServicio? _orderHeadersServicio;
        private readonly IShoeSizesServicio _shoeSizesServicio;
        private readonly IMapper? _mapper;

        public OrdersController(IOrderHeadersServicio? orderHeadersServicio,
            IShoeSizesServicio shoeSizesServicio, IMapper? mapper)
        {
            _orderHeadersServicio = orderHeadersServicio;
            _shoeSizesServicio = shoeSizesServicio;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var orderHeader = _orderHeadersServicio!.Get(
                filter: o => o.OrderHeaderId == id,
                propertiesNames: "OrderDetails.shoeSize.shoe,OrderDetails.shoeSize.size,OrderDetails.ShoeColor.Color"
            );

            foreach (var detail in orderHeader!.OrderDetails)
            {
                var shoeSizeInDetail = _shoeSizesServicio.Get(filter: ss => ss.ShoeSizeId == detail.ShoeSizeId);
                detail.shoeSize = shoeSizeInDetail;
            }
            return View(orderHeader);
        }
        #region API CALLS
        [HttpGet]
        public JsonResult GetAll()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity!;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var orderList = _orderHeadersServicio!.GetAll(filter: o => o.ApplicationUserId == claims!.Value);
            return Json(new { data = orderList });
        }
        #endregion

    }
}
