using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Interfaces
{
    public interface IGenreServicio
    {
		IEnumerable<Genre>? GetAll(Expression<Func<Genre, bool>>? filter = null,
		 Func<IQueryable<Genre>, IOrderedQueryable<Genre>>? orderBy = null, string? propertiesNames = null);
		void Save(Genre genre);
		void Delete(Genre genre);
		Genre? Get(Expression<Func<Genre, bool>>? filter = null, string? propertiesNames = null,
		bool tracked = true);
		bool Exist(Genre genre);
		bool ItsRelated(int id);
	}
}
