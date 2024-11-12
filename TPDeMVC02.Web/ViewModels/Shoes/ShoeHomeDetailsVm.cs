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
        public List<SelectListItem>? ListColor{ get; set; }
        public ShoeSize shoeSize { get; set; } = null!;
        public int SizeId { get; set; }
        public int ColorId { get; set; }


    }
}
