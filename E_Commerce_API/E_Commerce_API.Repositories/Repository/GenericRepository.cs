using E_Commerce_API.DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;
using Product.DataAccess;
using Product.Models;
using System.Linq.Expressions;


namespace Product_API.DataAccess.Repository
{
    public class GenericRepository<T>(ApplicationDbContext dbContext) : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly DbSet<T> _dbSet = dbContext.Set<T>();

        #region Create
        public async Task<T> Create(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await Save();
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Create: {ex.Message}");
                throw;
            }
        }
        #endregion

        #region Delete
        public async Task<bool> Delete(int id)
        {
            try
            {
                T entity = await _dbSet.FindAsync(id);

                if (entity is BaseEntity baseEntity)
                {
                    baseEntity.IsDelete = 1;
                    baseEntity.UpdatedAt = DateTime.Now;

                    _dbSet.Update(entity);
                    await Save();
                    return true;
                }
                return false;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error in Delete: {ex.Message}");
                throw;
            }
        }
        #endregion

        #region GetAll
        public async Task<IEnumerable<T>> GetAll(string? includeProperties = "")
        {
            try
            {
                IQueryable<T> query = _dbSet.Where(static e => (e as BaseEntity).IsDelete == 0);
                if (!string.IsNullOrWhiteSpace(includeProperties))
                {
                    foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp.Trim());
                    }
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAll: {ex.Message}");
                return [];
            }
        }
        #endregion

        #region GetData
        public async Task<T> GetData(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = _dbSet.Where(static e => (e as BaseEntity).IsDelete == 0);
                if (!string.IsNullOrWhiteSpace(includeProperties))
                {
                    foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp.Trim());
                    }
                }
                return await query.AsNoTracking().FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetData: {ex.Message}");
                throw;
            }
        }
        #endregion

        #region IsValueExit
        public async Task<bool> IsValueExit(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.Where(static e => (e as BaseEntity).IsDelete == 0).Where(predicate).AnyAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in IsValueExit: {ex.Message}");
                throw;
            }
        }
        #endregion

        #region Save
        public async Task Save()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Save: {ex.Message}");
                throw;
            }
        }
        #endregion

        #region Update
        public async Task<T> Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await Save();
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Update: {ex.Message}");
                throw;
            }
        }
        #endregion
    }
}
