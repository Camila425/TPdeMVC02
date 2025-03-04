using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
    public class ShoeImagesRepositorio : GenericRepository<ShoeImage>, IShoeImagesRepositorio
    {
        private readonly ShoesDbContext _dbContext;

        public ShoeImagesRepositorio(ShoesDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public bool Exist(ShoeImage Image)
        {
            throw new NotImplementedException();
        }

        public List<ShoeImage> GetShoeByShoeImageId(int shoeId)
        {
            return _dbContext.ShoeImages.Where(img => img.ShoeId == shoeId).ToList();
        }

        public bool ItsRelated(int id)
        {
            return _dbContext.shoes.Any(s => s.ShoeId == id);
        }

        public void Update(ShoeImage image)
        {
            _dbContext.ShoeImages.Update(image);

        }

    }
}
