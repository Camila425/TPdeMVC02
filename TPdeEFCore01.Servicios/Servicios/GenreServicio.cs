using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;
using System.Linq.Expressions;

namespace TPdeEFCore01.Servicios.Servicios
{
    public class GenreServicio : IGenreServicio
    {
        private readonly IGenresRepositorio _repository;
        private readonly IUnitOfWork _unitOfWork;
        public GenreServicio(IGenresRepositorio repository, IUnitOfWork unitOfWork)
        {
			_repository = repository ?? throw new ArgumentException("Dependencies not set");
			_unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
		}
		public void Delete(Genre genre)
		{
			try
			{
				_unitOfWork!.BeginTransaction();
				_repository!.Delete(genre);
				_unitOfWork!.Commit();

			}
			catch (Exception)
			{
				_unitOfWork!.Rollback();
				throw;
			}
		}

		public bool Exist(Genre genre)
		{
			return _repository!.Exist(genre);
		}

		public Genre? Get(Expression<Func<Genre, bool>>? filter = null,
			string? propertiesNames = null, bool tracked = true)
		{
			return _repository!.Get(filter, propertiesNames, tracked);
		}

		public IEnumerable<Genre>? GetAll(Expression<Func<Genre, bool>>? filter = null,
			Func<IQueryable<Genre>, IOrderedQueryable<Genre>>? orderBy = null, string? propertiesNames = null)
		{
			return _repository!.GetAll(filter, orderBy, propertiesNames);

		}

		public bool ItsRelated(int id)
		{
			return _repository!.ItsRelated(id);
		}

		public void Save(Genre genre)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				if (genre.GenreId == 0)
				{
					_repository?.Add(genre);
				}
				else
				{
					_repository?.Update(genre);
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
