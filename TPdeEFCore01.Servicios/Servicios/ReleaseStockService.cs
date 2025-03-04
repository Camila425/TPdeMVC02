using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Datos.Interfaces;

namespace TPdeEFCore01.Servicios.Servicios
{
    public class ReleaseStockService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ReleaseStockService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var cartsRepositorio = scope.ServiceProvider.GetRequiredService<IShoppingCartsRepositorio>();
                    var shoeSizeRepositorio = scope.ServiceProvider.GetRequiredService<IShoeSizesRepositorio>();
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    unitOfWork.BeginTransaction();
                    try
                    {
                        DateTime cutoffTime = DateTime.Now.AddHours(-1);
                        var inactiveCarts = cartsRepositorio.GetAll(filter: c => c.LastUpdated <= cutoffTime);

                        foreach (var cart in inactiveCarts)
                        {
                            var shoeSizeInCart = shoeSizeRepositorio.Get(filter: s => s.ShoeSizeId == cart.ShoeSizeId);
                            shoeSizeInCart!.StockInCarts -= cart.Quantity;
                            shoeSizeRepositorio.Update(shoeSizeInCart);
                            cartsRepositorio.Delete(cart); 
                        }

                        unitOfWork.Commit(); 
                    }
                    catch (Exception)
                    {
                        unitOfWork.Rollback(); 
                        throw;
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
        }
    }

}
