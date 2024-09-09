using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Interfaces
{
	public interface ISizeServicio
	{
		IEnumerable<Size>? GetAll(Expression<Func<Size, bool>>? filter = null,
			 Func<IQueryable<Size>, IOrderedQueryable<Size>>? orderBy = null, string? propertiesNames = null);
		void Save(Size size);
		void Delete(Size size);
		Size? Get(Expression<Func<Size, bool>>? filter = null, string? propertiesNames = null,
		bool tracked = true);
		bool Exist(Size size);
		bool ItsRelated(int id);
	}
}
