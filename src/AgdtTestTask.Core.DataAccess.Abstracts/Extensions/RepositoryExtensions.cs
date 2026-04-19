using AgdtTestTask.Core.Common.Helpers;
using AgdtTestTask.Core.Entities.Interfaces;

namespace AgdtTestTask.Core.DataAccess.Abstracts.Extensions
{
    public static class RepositoryExtensions
    {
        public static ValueTask<T> GetRequiredAsync<T>(
            this IRepository<T> repository,
            long id)
            where T : class, IIdentifiableEntity
        {
            return QueryingHelper.GetRequiredAsync(
                async () => await repository.GetAsync(id));
        }
    }
}
