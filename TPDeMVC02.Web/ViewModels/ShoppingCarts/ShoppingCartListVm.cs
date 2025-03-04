using TPdeEFCore01.Entidades;
using TPDeMVC02.Web.ViewModels.OrderHeaders;

namespace TPDeMVC02.Web.ViewModels.ShoppingCarts
{
    public class ShoppingCartListVm
    {
        public List<ShoppingCart>? ShoppingCarts { get; set; } 
        public OrderHeaderEditVm? OrderHeader { get; set; }
    }
}
