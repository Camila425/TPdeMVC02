using Microsoft.EntityFrameworkCore;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos
{
    public class ShoesDbContext : DbContext
    {
        public ShoesDbContext()
        {
                
        }
        public ShoesDbContext(DbContextOptions<ShoesDbContext> options)
      : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.; Initial Catalog=TPEFCore01; 
                Trusted_Connection=true;
                TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
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

            modelBuilder.Entity<ShoeSizes>(entity =>
            {
                entity.HasKey(ss => new {
                    ss.ShoeId,
                    ss.SizeId
                });

                entity.HasOne(ss => ss.shoe)
                    .WithMany(s => s.shoeSizes)
                    .HasForeignKey(ss => ss.ShoeId);

                entity.HasOne(ss => ss.size)
                    .WithMany(s=> s.shoeSizes)
                    .HasForeignKey(ss => ss.SizeId);

            });
            modelBuilder.Entity<Shoe>().HasData(shoeList);
          
        
    }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Shoe> shoes { get; set; }

        public DbSet<Size> Sizes { get; set; }
        public DbSet<ShoeSizes> shoeSizes { get; set; }


    }
}
