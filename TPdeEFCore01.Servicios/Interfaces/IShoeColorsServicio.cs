using System.Linq.Expressions;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Entidades.Dtos;

namespace TPdeEFCore01.Servicios.Interfaces
{
	public interface IShoeColorsServicio
	{
		IEnumerable<ShoeColor>? GetAll(Expression<Func<ShoeColor, bool>>? filter = null,
				   Func<IQueryable<ShoeColor>, IOrderedQueryable<ShoeColor>>? orderBy = null,
				   string? propertiesNames = null);
		ShoeColor? Get(Expression<Func<ShoeColor, bool>> filter,
			bool tracked = true, string? propertiesNames = null);
		void Save(ShoeColor shoecolor);
		void Remove(ShoeColor shoecolor);
		bool Exist(ShoeColor shoecolor);
		bool ItsRelated(int id);
		void AssignColorsAndPricesToShoe(ShoeColorDto shoeColor);
        decimal GetPriceByColor(int shoeId, int colorId);
    }
}
