using System.Linq.Expressions;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Entidades.Dtos;
using TPdeEFCore01.Servicios.Interfaces;

namespace TPdeEFCore01.Servicios.Servicios
{
	public class ShoeColorsServicio : IShoeColorsServicio
	{
		private readonly IShoeColorsRepositorio? _repository;
		private readonly IUnitOfWork? _unitOfWork;

		public ShoeColorsServicio(IShoeColorsRepositorio? repository, IUnitOfWork? unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public void Remove(ShoeColor shoeColor)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				_repository?.Delete(shoeColor);
				_unitOfWork?.Commit();
			}
			catch (Exception)
			{
				_unitOfWork?.Rollback();
				throw;
			}
		}

		public bool Exist(ShoeColor shoeColor)
		{
			if (_repository is null)
			{
				throw new ApplicationException("Dependencies not set");
			}
			return _repository.Exist(shoeColor);
		}
		public IEnumerable<ShoeColor>? GetAll(Expression<Func<ShoeColor, bool>>? filter = null,
			Func<IQueryable<ShoeColor>, IOrderedQueryable<ShoeColor>>? orderBy = null,
		string? propertiesNames = null)
		{
			return _repository?.GetAll(filter, orderBy, propertiesNames);
		}

		public ShoeColor? Get(Expression<Func<ShoeColor, bool>> filter,
			bool tracked = true, string? propertiesNames = null)
		{
			return _repository?.Get(filter, propertiesNames, tracked);
		}

		public bool ItsRelated(int id)
		{
			if (_repository is null)
			{
				throw new ApplicationException("Dependencies not set");
			}

			return _repository.ItsRelated(id);
		}

		public void Save(ShoeColor shoeColour)
		{
			try
			{
				_unitOfWork?.BeginTransaction();
				if (shoeColour.ShoeColorId == 0)
				{
					_repository?.Add(shoeColour);
				}
				else
				{
					_repository?.Update(shoeColour);
				}
				_unitOfWork?.Commit();

			}
			catch (Exception)
			{
				_unitOfWork?.Rollback();
				throw;
			}
		}

		public void AssignColorsAndPricesToShoe(ShoeColorDto shoeColor)
		{
			try
			{
				_unitOfWork?.BeginTransaction();

				ShoeColor shoeColourToSave = new ShoeColor()
				{
					ShoeId = shoeColor.ShoeId,
					ColorId = shoeColor.ColorId,
					PriceAdjustment = shoeColor.Price,
				};
				_repository?.Add(shoeColourToSave);
				_unitOfWork?.Commit();

			}
			catch (Exception)
			{
				_unitOfWork?.Rollback();
				throw;
			}


		}

        public decimal GetPriceByColor(int shoeId, int colorId)
        {
            return _repository!.GetPriceByColor(shoeId,colorId);
        }
    }
}
