using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
	public class GenreRepositorio : GenericRepository<Genre>, IGenresRepositorio
	{
		private readonly ShoesDbContext _dbContext;
		public GenreRepositorio(ShoesDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
		}

		public bool Exist(Genre genre)
		{
			if (genre.GenreId == 0)
			{
				return _dbContext.Genres.Any(g => g.GenreName == genre.GenreName);
			}
			return _dbContext.Genres.Any(g => g.GenreName ==
			genre.GenreName && g.GenreId != genre.GenreId);
		}

		public bool ItsRelated(int id)
		{
			return _dbContext.shoes.Any(g => g.GenreId == id);

		}

		public void Update(Genre genre)
		{
			_dbContext.Genres.Update(genre);
		}
	}
}
