using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Servicios.Interfaces;
using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Servicios
{
    public class StatesServicio:IStatesServicio
    {
        private readonly IStatesRepositorio? _statesRepositorio;
        private readonly IUnitOfWork? _unitOfWork;
        public StatesServicio(IStatesRepositorio? repository,
          IUnitOfWork? unitOfWork)
        {
            _statesRepositorio = repository ?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
        }
        public void Delete(State state)
        {
            try
            {
                _unitOfWork!.BeginTransaction();
                _statesRepositorio!.Delete(state);
                _unitOfWork!.Commit();

            }
            catch (Exception)
            {
                _unitOfWork!.Rollback();
                throw;
            }
        }


        public bool Exist(State state)
        {

            return _statesRepositorio!.Exist(state);
        }

        public State? Get(Expression<Func<State, bool>>? filter = null, 
            string? propertiesNames = null, bool tracked = true)
        {
            return _statesRepositorio!.Get(filter, propertiesNames, tracked);
        }


        public IEnumerable<State> GetAll(Expression<Func<State, bool>>? filter = null,
            Func<IQueryable<State>, IOrderedQueryable<State>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _statesRepositorio!.GetAll(filter, orderBy, propertiesNames);
        }


        public bool ItsRelated(int id)
        {

            return _statesRepositorio!.ItsRelated(id);
        }

        public void Save(State state)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (state.StateId == 0)
                {
                    _statesRepositorio?.Add(state);
                }
                else
                {
                    _statesRepositorio?.Update(state);
                }
                _unitOfWork?.Commit();

            }
            catch (Exception)
            {
                _unitOfWork?.Rollback();
                throw;
            }
        }
    }
}
