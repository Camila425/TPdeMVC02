using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace TPDeMVC02.Web.ViewModels.Shoes
{
    public class ShoeFilterVm
    {
        public IPagedList<ShoeListVm>? Shoes { get; set; }
        public List<SelectListItem>? Brands { get; set; }
		public List<SelectListItem>? Colors { get; set; }


	}
}
