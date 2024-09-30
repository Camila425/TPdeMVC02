using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
	public class BrandRepositorio : GenericRepository<Brand>, IBrandsRepositorio
	{
		private readonly ShoesDbContext _dbContext;
		public BrandRepositorio(ShoesDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
		}


		public bool Exist(Brand brand)
		{
			if (brand.BrandId == 0)
			{
				return _dbContext.Brands.Any(b => b.BrandName == brand.BrandName);
			}
			return _dbContext.Brands.Any(b => b.BrandName ==
			brand.BrandName && b.BrandId != brand.BrandId);
		}

		public bool ItsRelated(int id)
		{
			return _dbContext.shoes.Any(s => s.BrandId == id);
		}

		public void Update(Brand brand)
		{
			_dbContext.Brands.Update(brand);
		}
	}
}
