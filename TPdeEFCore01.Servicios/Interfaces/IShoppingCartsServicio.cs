using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Interfaces
{
    public interface IShoppingCartsServicio
    {
        IEnumerable<ShoppingCart>? GetAll(Expression<Func<ShoppingCart, bool>>? filter = null,
          Func<IQueryable<ShoppingCart>, IOrderedQueryable<ShoppingCart>>? orderBy = null,
          string? propertiesNames = null);
        void Save(ShoppingCart shoppingCart);
        void Delete(ShoppingCart shoppingCart);
        ShoppingCart? Get(Expression<Func<ShoppingCart, bool>>? filter = null,
            string? propertiesNames = null,
            bool tracked = true);
    }
}
