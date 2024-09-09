using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TPdeEFCore01.Datos;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Datos.Repositorios;
using TPdeEFCore01.Servicios.Interfaces;
using TPdeEFCore01.Servicios.Repositorios;
using TPdeEFCore01.Servicios.Servicios;

namespace TPdeEFCore01.Iol
{
	public class DI
	{
		public static void ConfigurarServicios(IServiceCollection servicios, IConfiguration configuration)
		{
			servicios.AddScoped<IBrandsRepositorio, BrandRepositorio>();
			servicios.AddScoped<IBrandService, BrandServicio>();

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

			servicios.AddDbContext<ShoesDbContext>(opciones =>
			{
				opciones.UseSqlServer(
				   configuration.GetConnectionString("MyConnection"));
			});
		}

	}
}
