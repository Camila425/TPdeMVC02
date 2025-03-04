using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
	public interface IShoeColorsRepositorio : IGenericRepository<ShoeColor>
	{
		void Update(ShoeColor shoeColor);
		bool Exist(ShoeColor shoeColor);
		bool ItsRelated(int id);
		void AssignColorsAndPricesToShoe(ShoeColor shoeColor);
        decimal GetPriceByColor(int shoeId, int colorId);
    }
}
