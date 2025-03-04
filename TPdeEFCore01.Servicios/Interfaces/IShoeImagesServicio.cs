using System.Linq.Expressions;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Servicios.Interfaces
{
    public interface IShoeImagesServicio
    {
        IEnumerable<ShoeImage>? GetAll(Expression<Func<ShoeImage, bool>>? filter = null,
       Func<IQueryable<ShoeImage>, IOrderedQueryable<ShoeImage>>? orderBy = null,
       string? propertiesNames = null);
        ShoeImage? Get(Expression<Func<ShoeImage, bool>>? filter = null, string? propertiesNames = null,
        bool tracked = true);
        void Save(ShoeImage Image);
        void Delete(ShoeImage Image);
        bool Exist(ShoeImage Image);
        bool ItsRelated(int id);
    }
}
