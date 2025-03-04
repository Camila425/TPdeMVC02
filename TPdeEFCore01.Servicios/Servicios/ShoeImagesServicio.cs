using System.Linq.Expressions;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;

namespace TPdeEFCore01.Servicios.Servicios
{
    public class ShoeImagesServicio : IShoeImagesServicio
    {
        private readonly IShoeImagesRepositorio? shoeImagesRepositorio;
        private readonly IUnitOfWork? unitOfWork;

        public ShoeImagesServicio(IShoeImagesRepositorio? shoeImagesRepositorio, IUnitOfWork? unitOfWork)
        {
            this.shoeImagesRepositorio = shoeImagesRepositorio;
            this.unitOfWork = unitOfWork;
        }

        public bool Exist(ShoeImage Image)
        {
            return shoeImagesRepositorio!.Exist(Image);

        }

        public IEnumerable<ShoeImage>? GetAll(Expression<Func<ShoeImage,
            bool>>? filter = null, Func<IQueryable<ShoeImage>,
                IOrderedQueryable<ShoeImage>>? orderBy = null, string? propertiesNames = null)
        {
            return shoeImagesRepositorio?.GetAll(filter, orderBy, propertiesNames);
        }



        public bool ItsRelated(int id)
        {
            return shoeImagesRepositorio!.ItsRelated(id);
        }

        public void Delete(ShoeImage image)
        {
            try
            {
                unitOfWork?.BeginTransaction();
                shoeImagesRepositorio?.Delete(image);
                unitOfWork?.Commit();

            }
            catch (Exception)
            {
                unitOfWork?.Rollback();
                throw;
            }
        }

        public void Save(ShoeImage image)
        {
            try
            {
                shoeImagesRepositorio?.Add(image);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public ShoeImage? Get(Expression<Func<ShoeImage, bool>>? filter = null, string? propertiesNames = null,
            bool tracked = true)
        {
            return shoeImagesRepositorio?.Get(filter, propertiesNames, tracked);
        }
    }
}
