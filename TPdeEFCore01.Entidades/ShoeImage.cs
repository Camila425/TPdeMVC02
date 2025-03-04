namespace TPdeEFCore01.Entidades
{
    public class ShoeImage
    {
        public int ShoeImageId { get; set; }
        public int ShoeId { get; set; }
        public string? ImageUrl { get; set; }
        public int? Description { get; set; }
        public Shoe shoe { get; set; } = null!;
    }
}
