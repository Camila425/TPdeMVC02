using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Interfaces
{
    public interface IBrandsRepositorio:IGenericRepository<Brand>
    {
        void Update(Brand brand);
        bool Exist(Brand brand);
        bool ItsRelated(int id);
    }
}
