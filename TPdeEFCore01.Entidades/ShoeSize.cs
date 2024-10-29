namespace TPdeEFCore01.Entidades
{
    public class ShoeSize
    {
        public int ShoeSizeId { get; set; }
        public int ShoeId { get; set; }
        public Shoe shoe { get; set; } = null!;
        public int SizeId { get; set; }
        public Size size { get; set; } = null!;
        public int QuantityInStock { get; set; }
    }
}
