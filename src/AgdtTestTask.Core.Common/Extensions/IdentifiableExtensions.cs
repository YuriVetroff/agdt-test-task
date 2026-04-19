using AgdtTestTask.Core.Common.Interfaces;

namespace AgdtTestTask.Core.Common.Extensions
{
    public static class IdentifiableExtensions
    {
        public static bool IsTransient<T>(
            this IIdentifiable<T> identifiable)
        {
            return EqualityComparer<T>.Default.Equals(
                identifiable.Id,
                default(T));
        }
    }
}
