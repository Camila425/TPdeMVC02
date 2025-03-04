namespace TPdeEFCore01.Entidades
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderHeaderId { get; set; }
        public int ShoeSizeId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public OrderHeader? OrderHeader { get; set; }
        public ShoeSize? shoeSize { get; set; }
        public int ShoeColorId { get; set; } 
        public ShoeColor ShoeColor { get; set; } = null!;
    }
}
