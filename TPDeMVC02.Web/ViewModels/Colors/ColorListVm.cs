using System.ComponentModel;

namespace TPDeMVC02.Web.ViewModels.Colors
{
	public class ColorListVm
	{
		public int ColorId { get; set; }
		[DisplayName("Color Name")]
		public string ColorName { get; set; } = null!;
		public bool Active { get; set; } = true;
	}
}
