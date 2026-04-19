using AutoMapper;

namespace AgdtTestTask.Core.Mapping.Extensions
{
    public static class AutomapperExtensions
    {
        public static IEnumerable<T> MapCollection<T>(
            this IMapperBase mapper,
            IEnumerable<object> collection)
        {
            return collection.Select(mapper.Map<T>).ToList();
        }
    }
}
