using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
	public class ShoeSizesRepositorio : GenericRepository<ShoeSize>, IShoeSizesRepositorio
	{
		private readonly ShoesDbContext _dbContext;

		public ShoeSizesRepositorio(ShoesDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}

		public void AssignSizesAndStockToShoe(ShoeSize shoeSizes)
		{
			_dbContext.ShoeSizes.Add(shoeSizes);
		}

		public bool Exist(ShoeSize shoeSizes)
		{
			if (shoeSizes.ShoeSizeId == 0)
			{
				return _dbContext.ShoeSizes.Any(ss => ss.SizeId == shoeSizes.SizeId &&
				ss.QuantityInStock == shoeSizes.QuantityInStock);
			}
			return _dbContext.ShoeSizes.Any(ss => ss.SizeId == shoeSizes.SizeId &&
				ss.QuantityInStock == shoeSizes.QuantityInStock && ss.ShoeSizeId != shoeSizes.ShoeSizeId);
		}

		public bool ItsRelated(int id)
		{
			return _dbContext.ShoeSizes.Any(s => s.ShoeSizeId == id);
		}

        public void Update(ShoeSize shoeSizes)
		{
			_dbContext.ShoeSizes.Update(shoeSizes);

		}
	}
}
