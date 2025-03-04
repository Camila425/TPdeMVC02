namespace TPDeMVC02.Web.ViewModels.Shoes
{
    public class ShoeHomeIndexVm
    {
        public int ShoeId { get; set; }
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public decimal ListPrice { get; set; }
        public decimal CashPrice { get; set; }
        public bool Active { get; set; }
        public string? ImageUrl { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public decimal Stock { get; set; }


    }
}
