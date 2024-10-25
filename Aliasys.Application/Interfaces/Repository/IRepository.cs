using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aliasys.Persistence.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity obj);
        Task<TEntity> GetByGuidAsync(Guid id);
        Task<TEntity> GetByIdAsync(int id);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<DbSet<TEntity>> GetDbSet();
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string> includedProps = null);
        Task<IQueryable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null, IEnumerable<string> includedProps = null);
        Task UpdateAsync(TEntity obj);
        Task RemoveByIdAsync(int id);
        Task RemoveByGuidAsync(Guid id);
        Task RemoveRangeAsync(ICollection<TEntity> entities);
        Task<int> SaveChangesAsync();
    }
}
