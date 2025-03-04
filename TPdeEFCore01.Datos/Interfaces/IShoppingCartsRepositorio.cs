using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
    public interface IShoppingCartsRepositorio : IGenericRepository<ShoppingCart>
    {
        void Update(ShoppingCart shoppingCart);
    }
}
