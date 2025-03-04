using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
    public interface IOrderDetailsRepositorio : IGenericRepository<OrderDetail>
    {
        void Update(OrderDetail orderDetail);
    }
}
