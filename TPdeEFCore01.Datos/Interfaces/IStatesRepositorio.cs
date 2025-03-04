using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
    public interface IStatesRepositorio : IGenericRepository<State>
    {
        void Update(State state);
        bool Exist(State state);
        bool ItsRelated(int id);
    }
}
