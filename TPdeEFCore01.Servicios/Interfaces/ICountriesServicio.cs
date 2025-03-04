using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Interfaces
{
    public interface ICountriesServicio
    {
        IEnumerable<Country> GetAll(Expression<Func<Country, bool>>? filter = null,
           Func<IQueryable<Country>, IOrderedQueryable<Country>>? orderBy = null,
           string? propertiesNames = null);
        void Save(Country country);
        void Delete(Country country);
        Country? Get(Expression<Func<Country, bool>>? filter = null,
            string? propertiesNames = null,
            bool tracked = true);
        bool Exist(Country country);
        bool ItsRelated(int id);
    }
}
