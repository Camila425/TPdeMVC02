using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
    public interface IShoeImagesRepositorio : IGenericRepository<ShoeImage>
    {
        void Update(ShoeImage image);
        bool Exist(ShoeImage Image);
        bool ItsRelated(int id);
        List<ShoeImage> GetShoeByShoeImageId(int shoeId);
    }
}
