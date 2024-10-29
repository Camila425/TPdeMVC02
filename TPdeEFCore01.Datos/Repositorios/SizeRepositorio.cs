using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
	public class SizeRepositorio : GenericRepository<Size>, ISizeRepositorio
	{
		private readonly ShoesDbContext _dbContext;

		public SizeRepositorio(ShoesDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
		}

		public bool Exist(Size size)
		{
			if (size.SizeId == 0)
			{
				return _dbContext.Sizes.Any(s => s.SizeNumber == size.SizeNumber);
			}
			return _dbContext.Sizes.Any(s => s.SizeNumber ==
			size.SizeNumber && s.SizeId != size.SizeId);
		}

		public bool ItsRelated(int id)
		{
			return _dbContext.ShoeSizes.Any(s => s.SizeId == id);
		}

		public void Update(Size size)
		{
			_dbContext.Sizes.Update(size);
		}
	}
}
