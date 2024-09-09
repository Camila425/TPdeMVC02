using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TPdeEFCore01.Datos.Interfaces;

namespace TPdeEFCore01.Datos.Repositorios
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly ShoesDbContext _context;
		internal DbSet<T> dbSet { get; set; }
		public GenericRepository(ShoesDbContext? context)
		{
			_context = context ?? throw new ArgumentException("Dependencies not set");
			 dbSet = _context.Set<T>();
		}

		public void Add(T entity)
		{
			try
			{
				dbSet.Add(entity);
			}
			catch (Exception)
			{

				throw new Exception("Error while adding an entity");
			}
		}

		public void Delete(T entity)
		{
			try
			{
				dbSet.Remove(entity);
			}
			catch (Exception)
			{

				throw new Exception("Error while removing an entity");
			}
		}

		public T? Get(Expression<Func<T, bool>>? filter = null, string? propertiesNames = null, bool tracked = true)
		{
			IQueryable<T> query = dbSet.AsQueryable();
			if (!string.IsNullOrWhiteSpace(propertiesNames))
			{
				foreach (var property in propertiesNames.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property);
				}
			}
			if (filter != null)
			{
				query = query.Where(filter);
			}
			return tracked ? query.FirstOrDefault() : query.AsNoTracking().FirstOrDefault();
		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>,
			IOrderedQueryable<T>>? orderBy = null, string? propertiesNames = null)
		{
			IQueryable<T> query = dbSet.AsNoTracking();
			if (!string.IsNullOrWhiteSpace(propertiesNames))
			{
				foreach (var property in propertiesNames
					.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property);
				}
			}
			if (orderBy != null)
			{
				query = orderBy(query);
			}
			if (filter != null)
			{
				query = query.Where(filter);
			}
			return query.ToList();
		}
	}
}
