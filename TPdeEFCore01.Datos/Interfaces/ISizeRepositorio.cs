using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
	public interface ISizeRepositorio : IGenericRepository<Size>
	{
		void Update(Size size);
		bool Exist(Size size);
		bool ItsRelated(int id);
	}
}
