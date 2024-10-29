using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
	public interface IShoeSizesRepositorio : IGenericRepository<ShoeSize>
	{
		void Update(ShoeSize shoeSizes);
		bool Exist(ShoeSize shoeSizes);
		bool ItsRelated(int id);
		void AssignSizesAndStockToShoe(ShoeSize shoeSizes);
	}
}
