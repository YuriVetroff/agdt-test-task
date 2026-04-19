using AgdtTestTask.Core.Entities.Interfaces;
using System.Linq.Expressions;

namespace AgdtTestTask.Core.DataAccess.Abstracts
{
    public interface IRepository<T>
        where T : class, IIdentifiableEntity
    {
        ValueTask<T> GetAsync(long id);

        Task<IEnumerable<T>> WhereAsync(
            Expression<Func<T, bool>> predicate);

        Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> ApplyQueryAsync(
            Func<IQueryable<T>, IQueryable<T>> queryBuilder);

        Task<long> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        Task DeleteAsync(T entity);
        Task DeleteByIdAsync(long id);
        Task DeleteRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
    }
}
