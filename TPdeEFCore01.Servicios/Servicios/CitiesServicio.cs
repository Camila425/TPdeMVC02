using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Datos;
using System.Linq.Expressions;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;

namespace TPdeEFCore01.Servicios.Servicios
{
    public class CitiesServicio:ICitiesServicio
    {
        private readonly ICitiesRepositorio? _citiesRepositorio;
        private readonly IUnitOfWork? _unitOfWork;

        public CitiesServicio(ICitiesRepositorio? citiesRepositorio, IUnitOfWork? unitOfWork)
        {
            _citiesRepositorio = citiesRepositorio;
            _unitOfWork = unitOfWork;
        }
        public bool Exist(City city)
        {
            if (_citiesRepositorio is null)
            {
                throw new ApplicationException("Dependencies not set");
            }
            return _citiesRepositorio.Exist(city);
        }

        public IEnumerable<City> GetAll(Expression<Func<City, bool>>? filter = null,
            Func<IQueryable<City>, IOrderedQueryable<City>>? orderBy = null, string? propertiesNames = null)
        {
            if (_citiesRepositorio == null)
            {
                throw new ApplicationException("Dependencies not set");
            }
            return _citiesRepositorio.GetAll(filter, orderBy, propertiesNames);
        }

        public City? Get(Expression<Func<City, bool>> filter, string? propertiesNames = null, bool tracked = true)
        {
            if (_citiesRepositorio == null)
            {
                throw new ApplicationException("Dependencies not set");
            }

            return _citiesRepositorio.Get(filter, propertiesNames, tracked);
        }

        public bool ItsRelated(int cityId)
        {
            return _citiesRepositorio!.ItsRelated(cityId);
        }

        public void Remove(City city)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                _citiesRepositorio?.Delete(city);
                _unitOfWork?.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }

        }

        public void Save(City city)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (city.CityId == 0)
                {
                    _citiesRepositorio?.Add(city);
                }
                else
                {
                    _citiesRepositorio?.Update(city);
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
