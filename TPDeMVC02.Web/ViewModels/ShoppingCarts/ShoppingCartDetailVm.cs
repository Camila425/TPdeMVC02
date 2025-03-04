using TPdeEFCore01.Entidades;
using TPDeMVC02.Web.ViewModels.Shoes;

namespace TPDeMVC02.Web.ViewModels.ShoppingCarts
{
    public class ShoppingCartDetailVm
    {
        public int ShoppingCartId { get; set; }

        public int ShoeSizeId { get; set; }
        public int ColorId { get; set; }

        public int ShoeId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public ShoeHomeDetailsVm shoeHomeDetailsVm { get; set; } = null!;
        public ShoeSize shoeSize { get; set; } = null!;
        public ShoeColor shoeColor { get; set; } = null!;
    }
}
