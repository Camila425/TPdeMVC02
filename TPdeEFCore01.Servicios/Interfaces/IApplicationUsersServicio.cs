using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Interfaces
{
    public interface IApplicationUsersServicio
    {
        IEnumerable<ApplicationUser>? GetAll(Expression<Func<ApplicationUser, bool>>? filter = null,
           Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>>? orderBy = null,
           string? propertiesNames = null);
        void Save(ApplicationUser applicationUser);
        void Delete(ApplicationUser applicationUser);
        ApplicationUser? Get(Expression<Func<ApplicationUser, bool>>? filter = null,
            string? propertiesNames = null,
            bool tracked = true);
    }
}
