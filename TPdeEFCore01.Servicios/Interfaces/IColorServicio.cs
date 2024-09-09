using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Interfaces
{
    public interface IColorServicio
    {
		IEnumerable<Color>? GetAll(Expression<Func<Color, bool>>? filter = null,
		Func<IQueryable<Color>, IOrderedQueryable<Color>>? orderBy = null, string? propertiesNames = null);
		void Save(Color color);
		void Delete(Color color);
		Color? Get(Expression<Func<Color, bool>>? filter = null, string? propertiesNames = null,
		bool tracked = true);
		bool Exist(Color color);
		bool ItsRelated(int id);
	}
}
