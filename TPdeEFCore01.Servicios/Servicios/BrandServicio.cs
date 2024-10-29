using System.Linq.Expressions;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;

namespace TPdeEFCore01.Servicios.Repositorios
{
    public class BrandServicio : IBrandServicio
    {
        private readonly IBrandsRepositorio? _repository;
        private readonly IUnitOfWork _unitOfWork;
        public BrandServicio(IBrandsRepositorio? repository, IUnitOfWork unitOfWork)
        {
            _repository = repository?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
		}

		public void Delete(Brand brand)
		{
			try
			{
				_unitOfWork!.BeginTransaction();
				_repository!.Delete(brand);
				_unitOfWork!.Commit();

			}
			catch (Exception)
			{
				_unitOfWork!.Rollback();
				throw;
			}
		}

		public bool Exist(Brand brand)
		{
			return _repository!.Exist(brand);
		}

		public Brand? Get(Expression<Func<Brand, bool>>? filter = null,
			string? propertiesNames = null, bool tracked = true)
		{
			return _repository!.Get(filter, propertiesNames, tracked);
		}

		public IEnumerable<Brand>? GetAll(Expression<Func<Brand, bool>>? filter = null, Func<IQueryable<Brand>, IOrderedQueryable<Brand>>? orderBy = null, string? propertiesNames = null)
		{
			return _repository!.GetAll(filter, orderBy, propertiesNames);
		}

		public bool ItsRelated(int id)
		{
			return _repository!.ItsRelated(id);
		}

		public void Save(Brand brand)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				if (brand.BrandId == 0)
				{
					_repository?.Add(brand);
				}
				else
				{
					_repository?.Update(brand);
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
