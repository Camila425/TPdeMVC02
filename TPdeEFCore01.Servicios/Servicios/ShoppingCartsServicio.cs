using System.Linq.Expressions;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Datos.Repositorios;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;

namespace TPdeEFCore01.Servicios.Servicios
{
    public class ShoppingCartsServicio : IShoppingCartsServicio
    {
        private readonly IShoppingCartsRepositorio? _cartsRepositorio;
        private readonly IShoesRepositorio? _shoesRepositorio;
        private readonly IShoeSizesRepositorio? _shoeSizesRepositorio;

        private readonly IUnitOfWork? _unitOfWork;

        public ShoppingCartsServicio(IShoppingCartsRepositorio? cartsRepositorio,
            IShoesRepositorio? shoesRepositorio,
            IShoeSizesRepositorio? shoeSizesRepositorio, IUnitOfWork? unitOfWork)
        {
            _cartsRepositorio = cartsRepositorio ?? throw new ArgumentException("Dependencies not set");
            _shoesRepositorio = shoesRepositorio ?? throw new ArgumentException("Dependencies not set");
            _shoeSizesRepositorio = shoeSizesRepositorio ?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
        }
        public void Delete(ShoppingCart shoppingCart)
        {
            try
            {
                _unitOfWork!.BeginTransaction();
                _cartsRepositorio!.Delete(shoppingCart);
                _shoeSizesRepositorio!.Update(shoppingCart.ShoeSize!);
                _unitOfWork!.Commit();
            }
            catch (Exception)
            {
                _unitOfWork!.Rollback();
                throw;
            }
        }

        public ShoppingCart? Get(Expression<Func<ShoppingCart, bool>>? filter = null,
            string? propertiesNames = null, bool tracked = true)
        {
            return _cartsRepositorio!.Get(filter, propertiesNames, tracked);
        }


        public IEnumerable<ShoppingCart> GetAll(Expression<Func<ShoppingCart, bool>>? filter = null,
            Func<IQueryable<ShoppingCart>, IOrderedQueryable<ShoppingCart>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _cartsRepositorio!.GetAll(filter, orderBy, propertiesNames);
        }
        public void Save(ShoppingCart shoppingCart)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (shoppingCart.ShoppingCartId == 0)
                {
                    _cartsRepositorio?.Add(shoppingCart);
                }
                else
                {
                    _cartsRepositorio?.Update(shoppingCart);
                }
                _shoeSizesRepositorio?.Update(shoppingCart.ShoeSize); 
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
