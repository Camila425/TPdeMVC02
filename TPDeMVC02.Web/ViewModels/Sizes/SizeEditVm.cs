using System.ComponentModel;

namespace TPDeMVC02.Web.ViewModels.Sizes
{
	public class SizeEditVm
	{
		public int SizeId { get; set; }
		[DisplayName("Size Number")]
		public decimal SizeNumber { get; set; }
	}
}
