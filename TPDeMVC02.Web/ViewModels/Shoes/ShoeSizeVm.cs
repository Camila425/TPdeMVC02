namespace TPDeMVC02.Web.ViewModels.Shoes
{
	public class ShoeSizeVm
	{
        public int SizeId { get; set; }
        public decimal SizeNumber { get; set; }
        public int Stock { get; set; }
        public int StockInCarts { get; set; }

        public int AvailableStock { get; set; }

        public bool IsSelected { get; set; }
        public int ShoeSizeId { get; set; }
    }
}
