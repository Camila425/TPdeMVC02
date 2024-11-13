using System.Linq.Expressions;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Entidades.Dtos;

namespace TPdeEFCore01.Servicios.Interfaces
{
	public interface IShoeSizesServicio
	{
		IEnumerable<ShoeSize>? GetAll(Expression<Func<ShoeSize, bool>>? filter = null,
		   Func<IQueryable<ShoeSize>, IOrderedQueryable<ShoeSize>>? orderBy = null,
		   string? propertiesNames = null);
		ShoeSize? Get(Expression<Func<ShoeSize, bool>>? filter = null, string? propertiesNames = null,
		bool tracked = true);
		void Save(ShoeSize shoeSize);
		void Delete(ShoeSize shoeSize);
		bool Exist(ShoeSize shoeSize);
		bool ItsRelated(int id);
		void AssignSizesAndStockToShoe(ShoeSizeDto shoeSizeDto);
    }
}
