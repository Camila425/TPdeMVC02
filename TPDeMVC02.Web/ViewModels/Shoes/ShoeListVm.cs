namespace TPDeMVC02.Web.ViewModels.Shoes
{
	public class ShoeListVm
	{
		public int ShoeId { get; set; }
		public string Description { get; set; } = null!;
		public string Brand { get; set; } = null!;
		public string Sport { get; set; } = null!;
		public string Genre { get; set; } = null!;
		public string Model { get; set; } = null!;
		public decimal Price { get; set; }
        public bool Active { get; set; }
        public int QuantityInStock { get; set; }
	}
}
