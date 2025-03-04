using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
    public class OrderDetailsRepositorio : GenericRepository<OrderDetail>, IOrderDetailsRepositorio
    {
        private readonly ShoesDbContext _dbContext;

        public OrderDetailsRepositorio(ShoesDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public void Update(OrderDetail orderDetail)
        {
            _dbContext.OrderDetails.Update(orderDetail);
        }
    }
}
