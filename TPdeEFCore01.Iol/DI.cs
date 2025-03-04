using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Datos.Repositorios;
using TPdeEFCore01.Servicios.Interfaces;
using TPdeEFCore01.Servicios.Repositorios;
using TPdeEFCore01.Servicios.Servicios;
using TPDeMVC02.Utilities;

namespace TPdeEFCore01.Iol
{
	public class DI
	{
		public static void ConfigurarServicios(IServiceCollection servicios, IConfiguration configuration)
		{
			servicios.AddScoped<IBrandsRepositorio, BrandRepositorio>();
			servicios.AddScoped<IBrandServicio, BrandServicio>();

			servicios.AddScoped<IGenresRepositorio, GenreRepositorio>();
			servicios.AddScoped<IGenreServicio, GenreServicio>();

			servicios.AddScoped<ISportServicio, SportServicio>();
			servicios.AddScoped<ISportsRepositorio, SportRepositorio>();

			servicios.AddScoped<IColorServicio, ColorServicio>();
			servicios.AddScoped<IColorsRepositorio, ColorRepositorio>();

			servicios.AddScoped<IShoeServicio, ShoeServicio>();
			servicios.AddScoped<IShoesRepositorio, ShoeRepositorio>();

			servicios.AddScoped<IUnitOfWork, UnitOfWork>();
			servicios.AddScoped<ISizeServicio, SizeServicio>();
			servicios.AddScoped<ISizeRepositorio, SizeRepositorio>();
            servicios.AddScoped<IShoeSizesServicio, ShoeSizesServicio>();
            servicios.AddScoped<IShoeSizesRepositorio, ShoeSizesRepositorio>();
            servicios.AddScoped<IShoeImagesServicio, ShoeImagesServicio>();
            servicios.AddScoped<IShoeImagesRepositorio, ShoeImagesRepositorio>();

            servicios.AddScoped<ICountriesServicio, CountriesServicio>();
            servicios.AddScoped<ICountriesRepositorio, CountriesRepositorio>();

            servicios.AddScoped<IStatesServicio, StatesServicio>();
            servicios.AddScoped<IStatesRepositorio, StatesRepositorio>();

            servicios.AddScoped<ICitiesServicio, CitiesServicio>();
            servicios.AddScoped<ICitiesRepositorio, CitiesRepositorio>();
            servicios.AddScoped<IShoeColorsServicio, ShoeColorsServicio>();
            servicios.AddScoped<IShoeColorsRepositorio, ShoeColorsRepositorio>();
            servicios.AddScoped<IEmailSender, EmailSender>();
            servicios.AddScoped<IApplicationUsersServicio, ApplicationUsersServicio>();
            servicios.AddScoped<IApplicationUsersRepositorio, ApplicationUsersRepositorio>();
            servicios.AddScoped<IShoppingCartsServicio, ShoppingCartsServicio>();
            servicios.AddScoped<IShoppingCartsRepositorio, ShoppingCartsRepositorio>();

            servicios.AddScoped<IOrderHeadersRepositorio, OrderHeadersRepositorio>();
            servicios.AddScoped<IOrderDetailsRepositorio, OrderDetailsRepositorio>();
            servicios.AddScoped<IOrderHeadersServicio, OrderHeadersServicio>();

            servicios.AddDbContext<ShoesDbContext>(opciones =>
			{
				opciones.UseSqlServer(
				   configuration.GetConnectionString("MyConnection"));
			});
		}

	}
}
