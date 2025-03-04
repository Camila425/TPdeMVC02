namespace TPdeEFCore01.Entidades
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public int ShoeSizeId { get; set; }
        public int Quantity { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        public ShoeSize ShoeSize { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public int ShoeColorId { get; set; } 
        public ShoeColor ShoeColor { get; set; } = null!;
    }
}
