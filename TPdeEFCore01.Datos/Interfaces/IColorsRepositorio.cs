using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
    public interface IColorsRepositorio : IGenericRepository<Color>
	{
		void Update(Color color);
		bool Exist(Color color);
		bool ItsRelated(int id);
	}
}
