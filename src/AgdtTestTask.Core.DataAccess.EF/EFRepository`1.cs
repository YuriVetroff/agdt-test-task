using AgdtTestTask.Core.DataAccess.Abstracts;
using AgdtTestTask.Core.DataAccess.Abstracts.Extensions;
using AgdtTestTask.Core.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgdtTestTask.Core.DataAccess.EF
{
    // Public for possible override
    public class EFRepository<T>
        : IRepository<T>
        where T : class, IIdentifiableEntity
    {
        private readonly DbContext _context;
        protected readonly DbSet<T> _set;

        public EFRepository(DbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate)
        {
            var query = BuildSearchQuery(predicate);
            return query.FirstOrDefaultAsync();
        }

        public ValueTask<T> GetAsync(long id)
        {
            return _set.FindAsync(id);
        }

        public async Task<IEnumerable<T>> WhereAsync(
            Expression<Func<T, bool>> predicate)
        {
            var query = BuildSearchQuery(predicate);
            return await query.ToListAsync();
        }

        public async Task<long> AddAsync(T entity)
        {
            await _set.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _set.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(T entity)
        {
            _set.Remove(entity);
            return _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(long id)
        {
            var entity = await this.GetRequiredAsync(id);
            await DeleteAsync(entity);
        }

        public Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _set.RemoveRange(entities);
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(T entity)
        {
            _set.Update(entity);
            return _context.SaveChangesAsync();
        }

        public Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _set.UpdateRange(entities);
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> ApplyQueryAsync(
            Func<IQueryable<T>, IQueryable<T>> queryBuilder)
        {
            return await queryBuilder(_set.AsQueryable()).ToListAsync();
        }

        protected virtual IQueryable<T> BuildSearchQuery(
            Expression<Func<T, bool>> predicate = null)
        {
            var query = _set.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }
    }
}
