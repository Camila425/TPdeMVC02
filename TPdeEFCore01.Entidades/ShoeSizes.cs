using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TPdeEFCore01.Entidades
{
    [Index(nameof(ShoeId), nameof(SizeId), IsUnique = true)]
    public class ShoeSizes
    {
        [Key]
        public int ShoeSizeId { get; set; }
        public int ShoeId { get; set; }
        public Shoe shoe { get; set; } = null!;
        public int SizeId { get; set; }
        public Size size { get; set; } = null!;
        public int QuantityInStock { get; set; }
    }
}
