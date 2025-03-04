using Microsoft.EntityFrameworkCore;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
    public class ShoppingCartsRepositorio : GenericRepository<ShoppingCart>, IShoppingCartsRepositorio
    {
        private readonly ShoesDbContext _dbContext;

        public ShoppingCartsRepositorio(ShoesDbContext db) : base(db)
        {
            _dbContext = db ?? throw new ArgumentNullException(nameof(db));
        }
        public void Update(ShoppingCart shoppingCart)
        {
            _dbContext.ShoppingCarts.Update(shoppingCart);
        }
        public IEnumerable<ShoppingCart> GetInactiveCarts(DateTime cutoffTime)
        {
            return _dbContext.ShoppingCarts
                .Include(sc => sc.ShoeSize)
                .Where(sc => sc.ShoeSizeId == sc.ShoeSize.ShoeSizeId
                             && sc.LastUpdated < cutoffTime)
                .ToList();
        }
    }
}
