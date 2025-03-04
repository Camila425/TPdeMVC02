using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Interfaces
{
    public interface ICitiesServicio
    {
        bool Exist(City city);
        IEnumerable<City> GetAll(Expression<Func<City, bool>>? filter = null,
            Func<IQueryable<City>, IOrderedQueryable<City>>? orderBy = null,
            string? propertiesNames = null);
        City? Get(Expression<Func<City, bool>> filter,
            string? propertiesNames = null,
            bool tracked = true);
        bool ItsRelated(int cityId);
        void Remove(City city);
        void Save(City city);
    }
}
