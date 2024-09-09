using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TPdeEFCore01.Entidades
{
    [Index(nameof(BrandName),IsUnique =true)]
    public class Brand
    {
        public int BrandId { get; set; }

        [StringLength(50)]
        public string BrandName { get; set; } = null!;
        public ICollection<Shoe> shoes { get; set; } = new List<Shoe>();
        public bool Active { get; set; } = true; 

    }
}
