using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
    public class StatesRepositorio : GenericRepository<State>, IStatesRepositorio
    {
        private readonly ShoesDbContext? _dbContext;

        public StatesRepositorio(ShoesDbContext db) : base(db)
        {
            _dbContext = db ?? throw new ArgumentException("Dependencies not set");
        }
        public bool Exist(State state)
        {
            if (state.StateId == 0)
            {
                return _dbContext!.States.Any(s => s.StateName == state.StateName
                    && s.CountryId == state.CountryId);

            }
            return _dbContext!.States.Any(s => s.StateName == state.StateName
                    && s.CountryId == state.CountryId
                    && s.StateId != state.StateId);
        }

        public bool ItsRelated(int id)
        {
            return _dbContext!.Cities.Any(c => c.StateId == id);
        }

        public void Update(State state)
        {
            _dbContext!.States.Update(state);
        }
    }
}
