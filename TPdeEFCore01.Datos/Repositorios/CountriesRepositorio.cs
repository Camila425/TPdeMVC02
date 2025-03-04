using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
    public class CountriesRepositorio : GenericRepository<Country>, ICountriesRepositorio
    {
        private readonly ShoesDbContext _dbContext;

        public CountriesRepositorio(ShoesDbContext db) : base(db)
        {
            _dbContext = db ?? throw new ArgumentNullException(nameof(db));
        }
        public bool Exist(Country country)
        {
            if (country.CountryId == 0)
            {
                return _dbContext.Countries.Any(c => c.CountryName == country.CountryName);
            }
            return _dbContext.Countries.Any(c => c.CountryName == country.CountryName && c.CountryId != country.CountryId);
        }


        public bool ItsRelated(int id)
        {
            return _dbContext.States.Any(p => p.CountryId == id);
        }

        public void Update(Country country)
        {
            _dbContext.Countries.Update(country);
        }

    }
}
