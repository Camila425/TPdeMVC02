using System.Linq.Expressions;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;

namespace TPdeEFCore01.Servicios.Servicios
{
    public class ApplicationUsersServicio : IApplicationUsersServicio
    {
        private readonly IApplicationUsersRepositorio? _applicationUsersRepositorio;
        private readonly IUnitOfWork? _unitOfWork;

        public ApplicationUsersServicio(IApplicationUsersRepositorio? applicationUsersRepositorio,
            IUnitOfWork? unitOfWork)
        {
            _applicationUsersRepositorio = applicationUsersRepositorio
                ?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
        }

        public void Delete(ApplicationUser applicationUser)
        {
            try
            {
                _unitOfWork!.BeginTransaction();
                _applicationUsersRepositorio!.Delete(applicationUser);
                _unitOfWork!.Commit();

            }
            catch (Exception)
            {
                _unitOfWork!.Rollback();
                throw;
            }
        }


        public ApplicationUser? Get(Expression<Func<ApplicationUser, bool>>? filter = null, 
            string? propertiesNames = null, bool tracked = true)
        {
            return _applicationUsersRepositorio!.Get(filter, propertiesNames, tracked);
        }


        public IEnumerable<ApplicationUser> GetAll(Expression<Func<ApplicationUser, bool>>? filter = null,
            Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _applicationUsersRepositorio!.GetAll(filter, orderBy, propertiesNames);
        }

        public void Save(ApplicationUser applicationUser)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (applicationUser.Id == string.Empty)
                {
                    _applicationUsersRepositorio?.Add(applicationUser);
                }
                else
                {
                    _applicationUsersRepositorio?.Update(applicationUser);
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
