using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
    public interface IGenresRepositorio : IGenericRepository<Genre>
	{
		void Update(Genre genre);
		bool Exist(Genre genre);
		bool ItsRelated(int id);
	}
}
