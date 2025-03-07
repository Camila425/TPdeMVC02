﻿using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
    public class ShoeRepositorio : GenericRepository<Shoe>, IShoesRepositorio
    {
        private readonly ShoesDbContext _dbContext;
        public ShoeRepositorio(ShoesDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }

        public bool Exist(Shoe shoe)
        {
            if (shoe.ShoeId == 0)
            {
                return _dbContext.shoes.Any(s => s.Description == shoe.Description
                && s.BrandId == shoe.BrandId && s.GenreId == shoe.GenreId && s.SportId == shoe.SportId
                && s.Model == shoe.Model);
            }

            return _dbContext.shoes.Any(s => s.Description == shoe.Description
            && s.BrandId == shoe.BrandId && s.GenreId == shoe.GenreId && s.SportId == shoe.SportId &&
            s.Model == shoe.Model && s.ShoeId != shoe.ShoeId);
        }

        public List<int> GetAssignedColorsForShoe(Shoe shoe)
        {
            return _dbContext.ShoeColors.Where(sc => sc.ShoeId == shoe.ShoeId)
                    .Select(sc => sc.ColorId).ToList();
        }

        public List<int> GetAssignedSizeForShoe(Shoe shoe)
        {
            return _dbContext.ShoeSizes.Where(ss => ss.ShoeId == shoe.ShoeId)
                   .Select(ss => ss.SizeId).ToList();
        }

        public Shoe? GetShoeId(int shoeId)
        {
            return _dbContext.shoes.FirstOrDefault(s => s.ShoeId == shoeId);
        }

        public bool ItsRelated(int id)
        {
            return _dbContext.ShoeSizes.Any(s => s.ShoeId == id);
        }

        public void Update(Shoe shoe)
        {
            _dbContext.shoes.Update(shoe);
        }
    }
}
