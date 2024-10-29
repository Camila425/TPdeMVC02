using Microsoft.AspNetCore.Mvc.Rendering;

namespace TPDeMVC02.Web.ViewModels.Shoes
{
	public class ShoeAssignSizesVm
	{
        public int ShoeId { get; set; }
        public string Model { get; set; } = null!;
        public decimal BasePrice { get; set; }
        public List<ShoeSizeVm> AvailableSizes { get; set; } = new List<ShoeSizeVm>();
		public IEnumerable<SelectListItem> AllSizes { get; set; } = new List<SelectListItem>();
        public int NewSizeId { get; set; }
        public int? NewStock{ get; set; }



    }
}
