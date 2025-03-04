using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Interfaces
{
    public interface IOrderHeadersServicio
    {
        IEnumerable<OrderHeader>? GetAll(Expression<Func<OrderHeader, bool>>? filter = null,
           Func<IQueryable<OrderHeader>, IOrderedQueryable<OrderHeader>>? orderBy = null,
           string? propertiesNames = null);
        void Save(OrderHeader OrderHeader);
        void Delete(OrderHeader OrderHeader);
        OrderHeader? Get(Expression<Func<OrderHeader, bool>>? filter = null,
            string? propertiesNames = null,
            bool tracked = true);
    }
}
