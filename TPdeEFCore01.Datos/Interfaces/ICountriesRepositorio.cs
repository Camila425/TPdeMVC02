using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
    public interface ICountriesRepositorio : IGenericRepository<Country>
    {
        void Update(Country country);
        bool Exist(Country country);
        bool ItsRelated(int id);
    }
}
