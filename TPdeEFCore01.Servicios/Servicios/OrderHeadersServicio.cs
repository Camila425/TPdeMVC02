using System.Linq.Expressions;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Servicios.Interfaces;

namespace TPdeEFCore01.Servicios.Servicios
{
    public class OrderHeadersServicio : IOrderHeadersServicio
    {
        private readonly IOrderHeadersRepositorio? _orderHeadersRepositorio;
        private readonly IShoeSizesRepositorio? _shoeSizesRepositorio;
        private readonly IShoppingCartsRepositorio? _shoppingCartsRepositorio;
        private readonly IUnitOfWork? _unitOfWork;

        public OrderHeadersServicio(IOrderHeadersRepositorio? orderHeadersRepositorio,
            IShoeSizesRepositorio shoeSizesRepositorio,
            IShoppingCartsRepositorio shoppingCartsRepositorio,
            IUnitOfWork? unitOfWork)
        {
            _orderHeadersRepositorio = orderHeadersRepositorio ?? throw new ArgumentException("Dependencies not set");
            _shoeSizesRepositorio = shoeSizesRepositorio ?? throw new ArgumentException("Dependencies not set");
            _shoppingCartsRepositorio = shoppingCartsRepositorio ?? throw new ArgumentException("Dependencies not set");
            _unitOfWork = unitOfWork ?? throw new ArgumentException("Dependencies not set");
        }

        public void Delete(OrderHeader orderHeader)
        {
            try
            {
                _unitOfWork!.BeginTransaction();
                _orderHeadersRepositorio!.Delete(orderHeader);
                _unitOfWork!.Commit();

            }
            catch (Exception)
            {
                _unitOfWork!.Rollback();
                throw;
            }
        }
        public OrderHeader? Get(Expression<Func<OrderHeader, bool>>? filter = null,
            string? propertiesNames = null, bool tracked = true)
        {
            return _orderHeadersRepositorio!.Get(filter, propertiesNames, tracked);
        }


        public IEnumerable<OrderHeader> GetAll(Expression<Func<OrderHeader, bool>>? filter = null,
            Func<IQueryable<OrderHeader>, IOrderedQueryable<OrderHeader>>? orderBy = null,
            string? propertiesNames = null)
        {
            return _orderHeadersRepositorio!.GetAll(filter, orderBy, propertiesNames);
        }
        public void Save(OrderHeader orderHeader)
        {
            try
            {
                _unitOfWork?.BeginTransaction();
                if (orderHeader.OrderHeaderId == 0)
                {
                    _orderHeadersRepositorio?.Add(orderHeader);
                    foreach (var item in orderHeader.OrderDetails)
                    {
                        var shoeSizesInDetail = _shoeSizesRepositorio!
                            .Get(filter: ss => ss.ShoeSizeId == item.ShoeSizeId);

                        shoeSizesInDetail!.QuantityInStock -= item.Quantity;
                        shoeSizesInDetail.StockInCarts -= item.Quantity;
                        _shoeSizesRepositorio.Update(shoeSizesInDetail);

                        var shoppingCart = _shoppingCartsRepositorio!.Get(filter: sc => sc.ShoeSizeId == item.ShoeSizeId
                        && sc.ApplicationUserId == orderHeader.ApplicationUserId);

                        _shoppingCartsRepositorio.Delete(shoppingCart!);
                    }
                }
                else
                {
                    _orderHeadersRepositorio?.Update(orderHeader);
                }
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
