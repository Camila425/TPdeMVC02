using System.Linq.Expressions;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;

namespace TPdeEFCore01.Servicios.Servicios
{
	public class SizeServicio : ISizeServicio
	{
		private readonly ISizeRepositorio _repository;
		private readonly IUnitOfWork _unitOfWork;

		public SizeServicio(ISizeRepositorio repository, IUnitOfWork unitOfWork)
		{
			_repository = repository ?? throw new ArgumentException("Dependencies not set");
			_unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
		}

		public void Delete(Size size)
		{
			try
			{
				_unitOfWork!.BeginTransaction();
				_repository!.Delete(size);
				_unitOfWork!.Commit();

			}
			catch (Exception)
			{
				_unitOfWork!.Rollback();
				throw;
			}
		}

		public bool Exist(Size size)
		{
			return _repository!.Exist(size);
		}

		public Size? Get(Expression<Func<Size, bool>>? filter = null,
			string? propertiesNames = null, bool tracked = true)
		{
			return _repository!.Get(filter, propertiesNames, tracked);
		}

		public IEnumerable<Size>? GetAll(Expression<Func<Size, bool>>? filter = null,
			Func<IQueryable<Size>, IOrderedQueryable<Size>>? orderBy = null, string? propertiesNames = null)
		{
			return _repository!.GetAll(filter, orderBy, propertiesNames);
		}

		public bool ItsRelated(int id)
		{
			return _repository!.ItsRelated(id);
		}

		public void Save(Size size)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				if (size.SizeId == 0)
				{
					_repository?.Add(size);
				}
				else
				{
					_repository?.Update(size);
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
