using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
    public class ApplicationUsersRepositorio : GenericRepository<ApplicationUser>, IApplicationUsersRepositorio
    {
        private readonly ShoesDbContext _dbContext;

        public ApplicationUsersRepositorio(ShoesDbContext db) : base(db)
        {
            _dbContext = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Update(ApplicationUser applicationUser)
        {

            _dbContext.ApplicationUsers.Update(applicationUser);
        }
    }
}
