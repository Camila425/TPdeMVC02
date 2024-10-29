using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPdeEFCore01.Entidades
{
    public class Shoe
    {
        public int ShoeId { get; set; }

        public int BrandId { get; set; }
        public int SportId { get; set; }
        public int GenreId { get; set; }
        public int ColorId { get; set; }
        public Brand Brand { get; set; } = null!;
        public Sport Sport { get; set; } = null!;
        public Genre Genre { get; set; } = null!;
        public Color Color { get; set; } = null!;



        [StringLength(150)]

        public string Model { get; set; } = null!;
        [MaxLength]
        public string Description { get; set; } = null!;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public bool Active { get; set; } = true;
        public ICollection<ShoeSize> shoeSizes { get; set; } = new List<ShoeSize>();


    }
}
