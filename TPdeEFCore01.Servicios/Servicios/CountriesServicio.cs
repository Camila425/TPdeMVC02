using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Servicios.Interfaces;
using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Servicios
{
    public class CountriesServicio : ICountriesServicio
    {
        private readonly ICountriesRepositorio? _countriesRepositorio;
        private readonly IUnitOfWork? _unitOfWork;

        public CountriesServicio(ICountriesRepositorio? countriesRepositorio, IUnitOfWork? unitOfWork)
        {
            _countriesRepositorio = countriesRepositorio;
            _unitOfWork = unitOfWork;
        }

        public void Delete(Country country)
        {
            try
            {
                _unitOfWork!.BeginTransaction();
                _countriesRepositorio!.Delete(country);
                _unitOfWork!.Commit();

            }
            catch (Exception)
            {
                _unitOfWork!.Rollback();
                throw;
            }
        }


        public bool Exist(Country country)
        {

            return _countriesRepositorio!.Exist(country);
        }

        public Country? Get(Expression<Func<Country, bool>>? filter = null,
            string? propertiesNames = null, bool tracked = true)
        {
            return _countriesRepositorio!.Get(filter, propertiesNames, tracked);
        }


        public IEnumerable<Country> GetAll(Expression<Func<Country, bool>>? filter = null,
            Func<IQueryable<Country>, IOrderedQueryable<Country>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _countriesRepositorio!.GetAll(filter, orderBy, propertiesNames);
        }


        public bool ItsRelated(int id)
        {

            return _countriesRepositorio!.ItsRelated(id);
        }

        public void Save(Country country)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (country.CountryId == 0)
                {
                    _countriesRepositorio?.Add(country);
                }
                else
                {
                    _countriesRepositorio?.Update(country);
                }
                _unitOfWork?.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }
    }
}
