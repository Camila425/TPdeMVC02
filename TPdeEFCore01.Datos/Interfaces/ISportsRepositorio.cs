using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
    public interface ISportsRepositorio : IGenericRepository<Sport>
	{
		void Update(Sport sport);
		bool Exist(Sport sport);
		bool ItsRelated(int id);
	}
}
