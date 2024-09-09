using System.ComponentModel;

namespace TPDeMVC02.Web.ViewModels.Sports
{
	public class SportListVm
	{
		public int SportId { get; set; }
		[DisplayName("Sport Name")]
		public string SportName { get; set; } = null!;
		public bool Active { get; set; } = true;
	}
}
