using Microsoft.AspNetCore.Mvc.Rendering;
using TPdeEFCore01.Entidades;

namespace TPDeMVC02.Web.ViewModels.Shoes
{
    public class ShoeHomeDetailsVm
    {
        public int ShoeId { get; set; }
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public decimal ListPrice { get; set; }
        public decimal CashPrice { get; set; }
        public bool Active { get; set; }
        public string? ImageUrl { get; set; }
        public int Stock { get; set; }
        public List<SelectListItem>? ListSize { get; set; }
        
        public List<ShoeSizeVm>? ListSizewithStock { get; set; }

        public List<ShoeColor> ListColor { get; set; } = new List<ShoeColor>();
        public decimal AvailableStock { get; set; }
        public ShoeSize shoeSize { get; set; } = null!;
        public List<ShoeSize> ListShoeSizes { get; set; } = new List<ShoeSize>();
        public ICollection<ShoeImage> Images { get; set; } = new List<ShoeImage>();
        public int SizeId { get; set; }
        public int ShoeColorId { get; set; }
        public Dictionary<int, int> SizeStock { get; set; } = new Dictionary<int, int>();

    }
}
