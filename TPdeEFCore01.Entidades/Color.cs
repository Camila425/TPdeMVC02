using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TPdeEFCore01.Entidades
{
    [Index(nameof(ColorName), IsUnique = true)]

    public class Color
    {
        public int ColorId { get; set; }
        [StringLength(50)]

        public string ColorName { get; set; } = null!;
        public ICollection<Shoe> shoes { get; set; } = new List<Shoe>();
        public bool Active { get; set; } = true;


    }
}
