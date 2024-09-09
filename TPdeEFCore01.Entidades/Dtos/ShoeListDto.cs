namespace TPdeEFCore01.Entidades.Dtos
{
    public class ShoeListDto
    {
        public int ShoeId { get; set; }
        public string Descripcion { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public string SportName { get; set; } = null!;
        public string GenreName { get; set; } = null!;
        public string ColorName { get; set; } = null!;
        public string Model { get; set; } = null!;
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }



    }
}
