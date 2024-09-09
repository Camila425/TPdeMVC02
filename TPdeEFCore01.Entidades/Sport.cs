using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TPdeEFCore01.Entidades
{
    [Index(nameof(SportName), IsUnique = true)]

    public class Sport
    {
        public int SportId { get; set; }
        [StringLength(20)]
        public string SportName { get; set; } = null!;
        public ICollection<Shoe> shoes { get; set; } = new List<Shoe>();
        public bool Active { get; set; } = true;
    }
}
