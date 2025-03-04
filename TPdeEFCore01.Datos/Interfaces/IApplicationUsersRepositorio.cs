using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
    public interface IApplicationUsersRepositorio : IGenericRepository<ApplicationUser>
    {
        void Update(ApplicationUser applicationUser);
    }
}
