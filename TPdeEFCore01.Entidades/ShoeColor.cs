namespace TPdeEFCore01.Entidades
{
	public class ShoeColor
	{
		public int ShoeColorId { get; set; }
		public int ShoeId { get; set; }
		public Shoe Shoe { get; set; } = null!;
		public int ColorId { get; set; }
		public Color Color { get; set; } = null!;
		public decimal PriceAdjustment { get; set; }
	}
}
