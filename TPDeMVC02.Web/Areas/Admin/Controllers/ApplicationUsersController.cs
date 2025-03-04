using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;
using TPdeEFCore01.Servicios.Servicios;
using TPDeMVC02.Web.ViewModels.ApplicationUsers;
using X.PagedList.Extensions;

namespace TPDeMVC02.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ApplicationUsersController : Controller
    {
        private readonly IApplicationUsersServicio? _applicationUsers;
        private readonly IMapper? _mapper;
        public ApplicationUsersController(IApplicationUsersServicio? applicationUsers, IMapper mapper)
        {
            _applicationUsers = applicationUsers ?? throw new ApplicationException("Dependencies not set");
            _mapper = mapper ?? throw new ApplicationException("Dependencies not set"); ;
        }
        public IActionResult Index(int? page, string? searchTerm = null, bool viewAll = false, int pageSize = 10)
        {
            int pageNumber = page ?? 1;
            ViewBag.currentPageSize = pageSize;
            IEnumerable<ApplicationUser>? users;
            if (!viewAll)
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    users = _applicationUsers?
                        .GetAll(orderBy: o => o.OrderBy(c => c.LastName).ThenBy(c => c.FirstName),
                            filter: c => c.FirstName.Contains(searchTerm)
                            || c.LastName!.Contains(searchTerm),
                            propertiesNames: "Country,State,City");
                    ViewBag.currentSearchTerm = searchTerm;
                }
                else
                {
                    users = _applicationUsers?
                            .GetAll(orderBy: o => o.OrderBy(c => c.LastName).ThenBy(c => c.FirstName),
                        propertiesNames: "Country,State,City");
                }
            }
            else
            {
                users = _applicationUsers?
                        .GetAll(orderBy: o => o.OrderBy(c => c.LastName).ThenBy(c => c.FirstName),
                        propertiesNames: "Country,State,City");
            }

            var usersVm = _mapper?.Map<List<ApplicationUserListVm>>(users)
                    .ToPagedList(pageNumber, pageSize);
            return View(usersVm);
        }
        public IActionResult Details(string id)
        {
            var applicationUser = _applicationUsers!.Get(
                filter: o => o.Id == id,
                propertiesNames: "Country,State,City,OrderHeaders");


            return View(applicationUser);
        }

       
    }
}
