using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
    public interface IOrderHeadersRepositorio : IGenericRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
    }
}
