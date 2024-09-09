using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Interfaces
{
    public interface ISportServicio
    {
		IEnumerable<Sport>? GetAll(Expression<Func<Sport, bool>>? filter = null,
			  Func<IQueryable<Sport>, IOrderedQueryable<Sport>>? orderBy = null, string? propertiesNames = null);
		void Save(Sport sport);
		void Delete(Sport sport);
		Sport? Get(Expression<Func<Sport, bool>>? filter = null, string? propertiesNames = null,
		bool tracked = true);
		bool Exist(Sport sport);
		bool ItsRelated(int id);
	}
}
