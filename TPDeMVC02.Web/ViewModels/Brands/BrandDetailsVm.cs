using System.ComponentModel;
using TPDeMVC02.Web.ViewModels.Shoes;
using X.PagedList;

namespace TPDeMVC02.Web.ViewModels.Brands
{
	public class BrandDetailsVm
	{
		public int BrandId { get; set; }

		[DisplayName("Brand Name")]
		public string? BrandName { get; set; }
		[DisplayName("Shoe Quantity")]
		public int ShoesQuantity { get; set; }

		public IPagedList<ShoeListVm>? ShoesListVm { get; set; }

	}
}
