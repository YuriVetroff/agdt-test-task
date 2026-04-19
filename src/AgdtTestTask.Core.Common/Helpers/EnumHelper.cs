using System.Reflection;
using System.Runtime.Serialization;

namespace AgdtTestTask.Core.Common.Helpers
{
    public static class EnumHelper
    {
        public static T ParseByEnumMemberValue<T>(
            string value)
            where T : struct, Enum
        {
            foreach (var field in typeof(T).GetFields(
                BindingFlags.Public |
                BindingFlags.Static))
            {
                var attr = field.GetCustomAttribute<EnumMemberAttribute>();
                if (attr?.Value != null &&
                    string.Equals(attr.Value, value, StringComparison.OrdinalIgnoreCase))
                {
                    return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException(
                $"'{value}' is not a valid {typeof(T).Name} value");
        }
    }
}
