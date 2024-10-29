using Microsoft.EntityFrameworkCore;

namespace TPdeEFCore01.Entidades
{
    [Index(nameof(SizeNumber), IsUnique = true)]
    public class Size
    {
        public int SizeId { get; set; }
        public decimal SizeNumber { get; set; }
        public ICollection<ShoeSize> shoeSizes { get; set; } = new List<ShoeSize>();

    }
}
