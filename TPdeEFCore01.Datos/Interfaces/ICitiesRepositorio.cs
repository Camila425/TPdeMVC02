using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
    public interface ICitiesRepositorio : IGenericRepository<City>
    {
        bool Exist(City city);
        bool ItsRelated(int cityId);
        void Update(City city);
    }
}
