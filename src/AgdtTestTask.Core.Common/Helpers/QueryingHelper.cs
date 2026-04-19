namespace AgdtTestTask.Core.Common.Helpers
{
    public static class QueryingHelper
    {
        public static async ValueTask<T> GetRequiredAsync<T>(
            Func<ValueTask<T>> getMethod)
        {
            var entity = await getMethod();
            if (entity == null)
            {
                throw new ArgumentNullException(
                    $"Failed to find entity {typeof(T).FullName}");
            }

            return entity;
        }
    }
}
