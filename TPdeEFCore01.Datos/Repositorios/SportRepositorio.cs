using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
    public class SportRepositorio : GenericRepository<Sport>, ISportsRepositorio
    {
        private readonly ShoesDbContext _dbContext;
        public SportRepositorio(ShoesDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }

        public bool Exist(Sport sport)
        {
            if (sport.SportId == 0)
            {
                return _dbContext.Sports.Any(s => s.SportName == sport.SportName);
            }
            return _dbContext.Sports.Any(s => s.SportName ==
            sport.SportName && s.SportId != sport.SportId);
        }

        public bool ItsRelated(int id)
        {
            return _dbContext.shoes.Any(s => s.SportId == id);
        }

        public void Update(Sport sport)
        {
            _dbContext.Sports.Update(sport);
        }
    }
}
