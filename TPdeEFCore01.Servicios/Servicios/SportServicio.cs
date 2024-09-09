using System.Linq.Expressions;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;

namespace TPdeEFCore01.Servicios.Servicios
{
	public class SportServicio : ISportServicio
	{
		private readonly ISportsRepositorio _repository;
		private readonly IUnitOfWork _unitOfWork;

		public SportServicio(ISportsRepositorio repository, IUnitOfWork unitOfWork)
		{
			_repository = repository ?? throw new ArgumentException("Dependencies not set");
			_unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
		}

		public void Delete(Sport sport)
		{
			try
			{
				_unitOfWork!.BeginTransaction();
				_repository!.Delete(sport);
				_unitOfWork!.Commit();

			}
			catch (Exception)
			{
				_unitOfWork!.Rollback();
				throw;
			}
		}

		public bool Exist(Sport sport)
		{
			return _repository!.Exist(sport);
		}

		public Sport? Get(Expression<Func<Sport, bool>>? filter = null, string? propertiesNames = null, bool tracked = true)
		{
			return _repository!.Get(filter, propertiesNames, tracked);
		}

		public IEnumerable<Sport>? GetAll(Expression<Func<Sport, bool>>? filter = null, Func<IQueryable<Sport>, IOrderedQueryable<Sport>>? orderBy = null, string? propertiesNames = null)
		{
			return _repository!.GetAll(filter, orderBy, propertiesNames);
		}

		public bool ItsRelated(int id)
		{
			return _repository!.ItsRelated(id);
		}

		public void Save(Sport sport)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				if (sport.SportId == 0)
				{
					_repository?.Add(sport);
				}
				else
				{
					_repository?.Update(sport);
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
