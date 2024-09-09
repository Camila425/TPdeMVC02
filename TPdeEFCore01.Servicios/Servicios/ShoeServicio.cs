using System.Linq.Expressions;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;

namespace TPdeEFCore01.Servicios.Servicios
{
	public class ShoeServicio : IShoeServicio
	{
		private readonly IShoesRepositorio _repository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ISizeRepositorio _sizeRepositorio;

		public ShoeServicio(IShoesRepositorio repository, IUnitOfWork unitOfWork, ISizeRepositorio sizeRepositorio)
		{
			_repository = repository ?? throw new ArgumentException("Dependencies not set");
			_unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
			_sizeRepositorio = sizeRepositorio;
		}

		public void Delete(Shoe shoe)
		{
			try
			{
				_unitOfWork!.BeginTransaction();
				_repository!.Delete(shoe);
				_unitOfWork!.Commit();

			}
			catch (Exception)
			{
				_unitOfWork!.Rollback();
				throw;
			}
		}

		public bool Exist(Shoe shoe)
		{
			return _repository!.Exist(shoe);
		}

		public Shoe? Get(Expression<Func<Shoe, bool>>? filter = null, 
			string? propertiesNames = null, bool tracked = true)
		{
			return _repository!.Get(filter, propertiesNames, tracked);
		}

		public IEnumerable<Shoe>? GetAll(Expression<Func<Shoe, bool>>? filter = null, Func<IQueryable<Shoe>, IOrderedQueryable<Shoe>>? orderBy = null, string? propertiesNames = null)
		{
			return _repository!.GetAll(filter, orderBy, propertiesNames);
		}

		public bool ItsRelated(int id)
		{
			return _repository!.ItsRelated(id);
		}

		public void Save(Shoe shoe)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				if (shoe.ShoeId == 0)
				{
					_repository?.Add(shoe);
				}
				else
				{
					_repository?.Update(shoe);
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
