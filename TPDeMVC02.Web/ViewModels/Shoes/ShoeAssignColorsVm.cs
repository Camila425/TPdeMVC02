using Microsoft.AspNetCore.Mvc.Rendering;

namespace TPDeMVC02.Web.ViewModels.Shoes
{
	public class ShoeAssignColorsVm
	{
		public int ShoeId { get; set; }
		public string Model { get; set; } = null!;
		public decimal BasePrice { get; set; }

		public List<ShoeColorVm> AvailableColors { get; set; } = new List<ShoeColorVm>();
		public IEnumerable<SelectListItem> AllColors { get; set; } = new List<SelectListItem>();

		public int? NewColorId { get; set; }
		public decimal? NewColorPrice { get; set; }
	}
}
