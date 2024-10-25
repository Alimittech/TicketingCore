
using Aliasys.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aliasys.Persistence.Repository
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {

        private readonly DataBaseContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private bool disposed;
        public Repository(DataBaseContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            try
            {
                await _dbSet.AddAsync(obj);
                await SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IQueryable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            IEnumerable<string> includedProps = null)
        {
            var result = _dbSet.AsQueryable();
            if (includedProps != null)
                foreach (var includedProp in includedProps)
                    result = result.Include(includedProp);

            return
                predicate == null
                    ? await Task.Run(() => result)
                    : await Task.Run(() => result.Where(predicate));
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate,
            IEnumerable<string> includedProps = null)
        {
            var result = _dbSet.AsQueryable();

            if (includedProps != null)
                foreach (var includedProp in includedProps)
                    result = result.Include(includedProp);

            return await result.FirstOrDefaultAsync(predicate);
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            try
            {
                return await Task.Run(() => _dbSet.AsQueryable());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> GetByGuidAsync(Guid id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                //TODO: Write Log on System (Security issue)
                throw ex;
            }
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                //TODO: Write Log on System (Security issue)
                throw ex;
            }
        }

        public async Task RemoveByIdAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await SaveChangesAsync();
                    //TODO: check the remove done or not (check the return value of save changes async func)
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RemoveByGuidAsync(Guid id)
        {
            try
            {
                var entity = await GetByGuidAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await SaveChangesAsync();
                    //TODO: check the remove done or not (check the return value of save changes async func)
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveRangeAsync(ICollection<TEntity> entities)
        {
            try
            {
                _dbSet.RemoveRange(entities);
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task UpdateAsync(TEntity obj)
        {
            try
            {
                _dbSet.Update(obj);
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DbSet<TEntity>> GetDbSet()
        {
            return await Task.Run(() => _dbSet);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    _context.Dispose();

            disposed = true;
        }

    }

}
