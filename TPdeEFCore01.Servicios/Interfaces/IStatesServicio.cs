using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Interfaces
{
    public interface IStatesServicio
    {
        IEnumerable<State> GetAll(Expression<Func<State, bool>>? filter = null,
        Func<IQueryable<State>, IOrderedQueryable<State>>? orderBy = null,
        string? propertiesNames = null);
        void Save(State state);
        void Delete(State state);
        State? Get(Expression<Func<State, bool>>? filter = null,
            string? propertiesNames = null,
            bool tracked = true);
        bool Exist(State state);
        bool ItsRelated(int id);
    }
}
