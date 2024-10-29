using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Interfaces
{
	public interface IBrandServicio
	{
		IEnumerable<Brand>? GetAll(Expression<Func<Brand, bool>>? filter = null,
	    Func<IQueryable<Brand>, IOrderedQueryable<Brand>>? orderBy = null,string? propertiesNames = null);
		void Save(Brand brand);
		void Delete(Brand brand);
		Brand? Get(Expression<Func<Brand, bool>>? filter = null,string? propertiesNames = null,
	    bool tracked = true);
		bool Exist(Brand brand);
		bool ItsRelated(int id);
	}
}
