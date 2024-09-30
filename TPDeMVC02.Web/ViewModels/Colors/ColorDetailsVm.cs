using System.ComponentModel;
using TPDeMVC02.Web.ViewModels.Shoes;
using X.PagedList;

namespace TPDeMVC02.Web.ViewModels.Colors
{
	public class ColorDetailsVm
	{
		public int ColorId { get; set; }

		[DisplayName("Color Name")]
		public string? ColorName { get; set; }
		[DisplayName("Shoe Quantity")]
		public int ShoesQuantity { get; set; }

		public IPagedList<ShoeListVm>? ShoesListVm { get; set; }

	}
}
