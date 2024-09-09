using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;
using System.Linq.Expressions;

namespace TPdeEFCore01.Servicios.Servicios
{
    public class ColorServicio : IColorServicio
    {
        private readonly IColorsRepositorio _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ColorServicio(IColorsRepositorio repository, IUnitOfWork unitOfWork)
        {
			_repository = repository ?? throw new ArgumentException("Dependencies not set");
			_unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
		}

		public void Delete(Color color)
		{
			try
			{
				_unitOfWork!.BeginTransaction();
				_repository!.Delete(color);
				_unitOfWork!.Commit();

			}
			catch (Exception)
			{
				_unitOfWork!.Rollback();
				throw;
			}
		}

		public bool Exist(Color color)
		{
			return _repository!.Exist(color);
		}

		public Color? Get(Expression<Func<Color, bool>>? filter = null,
			string? propertiesNames = null, bool tracked = true)
		{
			return _repository!.Get(filter, propertiesNames, tracked);
		}

		public IEnumerable<Color>? GetAll(Expression<Func<Color, bool>>? filter = null,
			Func<IQueryable<Color>, IOrderedQueryable<Color>>? orderBy = null, string? propertiesNames = null)
		{
			return _repository!.GetAll(filter, orderBy, propertiesNames);

		}

		public bool ItsRelated(int id)
		{
			return _repository!.ItsRelated(id);
		}

		public void Save(Color color)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				if (color.ColorId == 0)
				{
					_repository?.Add(color);
				}
				else
				{
					_repository?.Update(color);
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
