using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos
{
    public class ShoesDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Shoe> shoes { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ShoeSize> ShoeSizes { get; set; }
		public DbSet<ShoeColor> ShoeColors { get; set; }
		public DbSet<ShoeImage> ShoeImages { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public ShoesDbContext(DbContextOptions<ShoesDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);
            var BrandList = new List<Brand>()
            {
                new Brand
                {
                    BrandId=1,
                    BrandName="Adidas",
                }, new Brand
                {
                    BrandId=2,
                    BrandName="Nike",
                },
                new Brand
                {
                    BrandId=3,
                    BrandName="Puma",
                },
            };
            modelBuilder.Entity<Brand>().HasData(BrandList);

            var GenreList = new List<Genre>()
            {
                new Genre
                {
                    GenreId=1,
                    GenreName="Correr",
                }, new Genre
                {
                    GenreId=2,
                    GenreName="Futbol",
                },
                new Genre
                {
                    GenreId=3,
                    GenreName="Baloncesto",
                },
            };
            modelBuilder.Entity<Genre>().HasData(GenreList);
            var SportList = new List<Sport>()
            {
                new Sport
                {
                    SportId=1,
                    SportName="Tennis",
                }, new Sport
                {
                    SportId=2,
                    SportName="Basketball",
                },
                new Sport
                {
                    SportId=3,
                    SportName="Volleyball",
                },
            };
            modelBuilder.Entity<Sport>().HasData(SportList);
            var ColorList = new List<Color>()
            {
                new Color
                {
                    ColorId=1,
                    ColorName="Rojo",
                }, new Color
                {
                    ColorId=2,
                    ColorName="Blanco",
                },
                new Color
                {
                    ColorId=3,
                    ColorName="Negro",
                },
            };
            modelBuilder.Entity<Color>().HasData(ColorList);
            var shoeList = new List<Shoe>()
            {
                new Shoe
                {
                    ShoeId=1,
                    BrandId=1,
                    SportId=1,
                    GenreId=1,
                    Model="Samba",
                    Description="con tres rayas blancas laterales",
                    Price=8999000
                }, new Shoe
                {

                    ShoeId=2,
                    BrandId=2,
                    SportId=2,
                    GenreId=2,
                    Model=" Air Max 270",
                    Description="Zapatillas para correr con tecnología",
                    Price=12999,
                },
                new Shoe
                {
                    ShoeId=3,
                    BrandId=3,
                    SportId=3,
                    GenreId=3,
                    Model="  clyde all-pro",
                    Description="Zapatillas de Volleyball",
                    Price=10999,
                },
            };

            modelBuilder.Entity<Size>()
           .HasIndex(s => s.SizeNumber)
           .IsUnique();

            modelBuilder.Entity<ShoeSize>(entity =>
            {
                entity.HasKey(ss => new {
                    ss.ShoeSizeId
                });

                entity.HasOne(ss => ss.shoe)
                    .WithMany(s => s.shoeSizes)
                    .HasForeignKey(ss => ss.ShoeId);

                entity.HasOne(ss => ss.size)
                    .WithMany(s=> s.shoeSizes)
                    .HasForeignKey(ss => ss.SizeId);

            });
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(ss => new {
                    ss.CountryId
                });
            });
            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(ss => new {
                    ss.StateId
                });
            });
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(ss => new {
                    ss.CityId
                });
            });
            modelBuilder.Entity<OrderHeader>(entity =>
            {
                entity.HasKey(ss => new {
                    ss.OrderHeaderId
                });
            });
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(ss => new {
                    ss.OrderDetailId
                });
            });
            modelBuilder.Entity<ShoeColor>(entity =>
            {
                entity.HasKey(ss => new {
                    ss.ShoeColorId
                });
            });
            modelBuilder.Entity<Shoe>().HasData(shoeList);
        }
    }
}
