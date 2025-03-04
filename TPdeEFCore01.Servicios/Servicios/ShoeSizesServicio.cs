using System.Linq.Expressions;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Entidades.Dtos;
using TPdeEFCore01.Servicios.Interfaces;

namespace TPdeEFCore01.Servicios.Servicios
{
	public class ShoeSizesServicio : IShoeSizesServicio
	{
		private readonly IShoeSizesRepositorio? shoeSizesRepositorio;
		private readonly IUnitOfWork? unitOfWork;

		public ShoeSizesServicio(IShoeSizesRepositorio? shoeSizesRepositorio, IUnitOfWork? unitOfWork)
		{
			this.shoeSizesRepositorio = shoeSizesRepositorio;
			this.unitOfWork = unitOfWork;
		}

        public void AssignSizesAndStockToShoe(ShoeSizeDto shoeSizeDto)
        {
            try
            {
                unitOfWork?.BeginTransaction();
                var existingShoeSize = shoeSizesRepositorio?.GetShoeSizeId(shoeSizeDto.ShoeId, shoeSizeDto.SizeId);

                if (existingShoeSize != null)
                {
                    existingShoeSize.QuantityInStock += shoeSizeDto.Stock; 
                    existingShoeSize.AvailableStock = existingShoeSize.QuantityInStock - existingShoeSize.StockInCarts;
                }
                else
                {
                    ShoeSize shoeSizesToSave = new ShoeSize()
                    {
                        ShoeId = shoeSizeDto.ShoeId,
                        SizeId = shoeSizeDto.SizeId,
                        QuantityInStock = shoeSizeDto.Stock,
                        AvailableStock = shoeSizeDto.Stock
                    };
                    shoeSizesRepositorio?.Add(shoeSizesToSave);
                }
                unitOfWork?.Commit();
            }
            catch (Exception)
            {
                unitOfWork?.Rollback();
                throw;
            }
        }

        public bool Exist(ShoeSize shoeSizes)
		{
			return shoeSizesRepositorio!.Exist(shoeSizes);

		}

		public IEnumerable<ShoeSize>? GetAll(Expression<Func<ShoeSize,
			bool>>? filter = null, Func<IQueryable<ShoeSize>,
				IOrderedQueryable<ShoeSize>>? orderBy = null, string? propertiesNames = null)
		{
			return shoeSizesRepositorio?.GetAll(filter,orderBy,propertiesNames);
		}
	
		public bool ItsRelated(int id)
		{
			return shoeSizesRepositorio!.ItsRelated(id);
		}

		public void Delete(ShoeSize shoeSizes)
		{
			try
			{
				unitOfWork?.BeginTransaction();
				shoeSizesRepositorio?.Delete(shoeSizes);
				unitOfWork?.Commit();

			}
			catch (Exception)
			{
				unitOfWork?.Rollback();
				throw;
			}
		}

		public void Save(ShoeSize shoeSizes)
		{
			try
			{
				unitOfWork?.BeginTransaction();
				if (shoeSizes.ShoeSizeId == 0)
				{
					shoeSizesRepositorio?.Add(shoeSizes);
				}
				else
				{
					shoeSizesRepositorio?.Update(shoeSizes);
				}
				unitOfWork?.Commit();

			}
			catch (Exception)
			{
				unitOfWork?.Rollback();
				throw;
			}
		}

		public ShoeSize? Get(Expression<Func<ShoeSize, bool>>? filter = null, string? propertiesNames = null,
			bool tracked = true)
		{
			return shoeSizesRepositorio?.Get(filter,propertiesNames,tracked);
		}

        public ShoeSize GetShoeSizeId(int? ShoeId, int? SizeId)
        {
			return shoeSizesRepositorio!.GetShoeSizeId(ShoeId,SizeId);
        }

    }
}
