using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
	public interface IShoesRepositorio : IGenericRepository<Shoe>
	{
		void Update(Shoe shoe);
		bool Exist(Shoe shoe);
		bool ItsRelated(int id);
		List<int> GetAssignedSizeForShoe(Shoe shoe);
    }
}
