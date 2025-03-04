using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
    public class CitiesRepositorio : GenericRepository<City>, ICitiesRepositorio
    {
        private readonly ShoesDbContext _context;
        public CitiesRepositorio(ShoesDbContext context) : base(context)
        {
            _context = context;
        }
        public bool Exist(City city)
        {
            return city.CityId == 0 ? _context.Cities.Any(c => c.CityName == city.CityName
                    && c.StateId == city.StateId
                    && c.CountryId == city.CountryId) : _context.Cities.Any(c => c.CityName == city.CityName
                    && c.StateId == city.StateId
                    && c.CountryId == city.CountryId
                    && c.CityId != city.CityId);
        }

        public bool ItsRelated(int cityId)
        {
            return false;
        }

        public void Update(City city)
        {
            _context.Update(city);
        }
    }
}
