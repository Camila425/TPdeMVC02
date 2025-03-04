using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
    public class OrderHeadersRepositorio : GenericRepository<OrderHeader>, IOrderHeadersRepositorio
    {
        private readonly ShoesDbContext _dbContext;

        public OrderHeadersRepositorio(ShoesDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public void Update(OrderHeader orderHeader)
        {
            _dbContext.OrderHeaders.Update(orderHeader);
        }
    }
}
