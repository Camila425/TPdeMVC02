using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Interfaces
{
	public interface IShoeServicio
	{
		IEnumerable<Shoe>? GetAll(Expression<Func<Shoe, bool>>? filter = null,
		   Func<IQueryable<Shoe>, IOrderedQueryable<Shoe>>? orderBy = null, string? propertiesNames = null);
		void Save(Shoe shoe);
		void Delete(Shoe shoe);
		Shoe? Get(Expression<Func<Shoe, bool>>? filter = null, string? propertiesNames = null,
		bool tracked = true);
		bool Exist(Shoe shoe);
		bool ItsRelated(int id);
	}
}
