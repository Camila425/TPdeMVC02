namespace TPDeMVC02.Web.ViewModels.Shoes
{
	public class ShoeColorVm
	{
		public int ColorId { get; set; }
		public string ColorName { get; set; } = null!;
		public decimal Price { get; set; } 
		public bool IsSelected { get; set; } 
	}
}
