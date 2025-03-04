using Microsoft.EntityFrameworkCore;
using TPdeEFCore01.Datos.Interfaces;
using TPdeEFCore01.Entidades;

namespace TPdeEFCore01.Datos.Repositorios
{
	public class ShoeColorsRepositorio : GenericRepository<ShoeColor>, IShoeColorsRepositorio
	{
		private readonly ShoesDbContext _context;

		public ShoeColorsRepositorio(ShoesDbContext context) : base(context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public void AssignColorsAndPricesToShoe(ShoeColor shoeColor)
		{
			_context.ShoeColors.Add(shoeColor);
		}

		public bool Exist(ShoeColor shoeColor)
		{
			if (shoeColor == null)
			{
				throw new ArgumentNullException(nameof(shoeColor));
			}

			if (shoeColor.ShoeColorId == 0)
			{
				return _context.ShoeColors.Any(c => c.ShoeId == shoeColor.ShoeId &&
							c.ColorId == shoeColor.ColorId);
			}
			return _context.ShoeColors.Any(c => c.ShoeId == shoeColor.ShoeId &&
							c.ColorId == shoeColor.ColorId &&
							c.ShoeColorId != shoeColor.ShoeColorId);
		}

        public decimal GetPriceByColor(int shoeId, int colorId)
        {
            var shoeColor = _context.ShoeColors.Include(sc => sc.Shoe)
            .FirstOrDefault(sc => sc.ColorId == colorId && sc.Shoe.ShoeId==shoeId);

            if (shoeColor == null) return 0;

            return shoeColor.Shoe.Price + shoeColor.PriceAdjustment;
        }
        public bool ItsRelated(int id)
		{
			return _context.ShoeColors.Any(p => p.ShoeColorId == id);
		}

		public void Update(ShoeColor shoeColor)
		{
			if (shoeColor == null)
			{
				throw new ArgumentNullException(nameof(shoeColor));
			}

			_context.ShoeColors.Update(shoeColor);

		}
	}
}
