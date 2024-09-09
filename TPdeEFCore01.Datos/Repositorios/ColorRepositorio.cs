using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
    public class ColorRepositorio : GenericRepository<Color>, IColorsRepositorio
	{
        private readonly ShoesDbContext _dbContext;
        public ColorRepositorio(ShoesDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
		}

		public bool Exist(Color color)
		{
			if (color.ColorId == 0)
			{
				return _dbContext.Colors.Any(c => c.ColorName == color.ColorName);
			}
			return _dbContext.Colors.Any(c => c.ColorName ==
			color.ColorName && c.ColorId != color.ColorId);
		}

		public bool ItsRelated(int id)
		{
			return _dbContext.shoes.Any(c => c.ColorId == id);
		}

		public void Update(Color color)
		{
			_dbContext.Colors.Update(color);
		}
	}
}
