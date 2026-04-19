using AgdtTestTask.Core.Common.Extensions;
using AgdtTestTask.Core.Common.Helpers;
using AgdtTestTask.Core.Common.Primitives;
using AgdtTestTask.Medical.Fhir.Enums;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace AgdtTestTask.Medical.Fhir.Extensions
{
    public static class FhirSearchExtensions
    {
        private static readonly Regex ParamRegex = new(
            @"^(?<prefix>eq|ne|gt|lt|ge|le|sa|eb|ap)?(?<date>\d{4}(-\d{2}(-\d{2}(T\d{2}:\d{2}(:\d{2})?)?)?)?)$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static IQueryable<T> BuildDateSearchExpression<T>(
            this IQueryable<T> query,
            IEnumerable<string> birthdateParams,
            Expression<Func<T, DateTime>> dateSelector)
        {
            if (birthdateParams == null)
            {
                return query;
            }

            var parsed = birthdateParams
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(Parse)
                .ToList();

            if (parsed.Count == 0)
            {
                return query;
            }

            var expressions = parsed
                .Select(p => BuildSearchExpression<T>(
                    p.Prefix, p.Interval.Start, p.Interval.End, dateSelector))
                .ToList();

            var predicate = expressions.Count == 1
                ? expressions[0]
                : expressions.CombineAnd();

            return query.Where(predicate);
        }

        private static Expression<Func<T, bool>> BuildSearchExpression<T>(
            FhirPrefix prefix,
            DateTime start,
            DateTime end,
            Expression<Func<T, DateTime>> dateSelector)
        {
            var parameter = dateSelector.Parameters[0];
            var date = dateSelector.Body;

            var startConst = Expression.Constant(start, typeof(DateTime));
            var endConst = Expression.Constant(end, typeof(DateTime));

            var inRange = InRange(date, startConst, endConst);

            var match = prefix switch
            {
                FhirPrefix.Equal =>
                    inRange,

                FhirPrefix.NotEqual =>
                    Expression.Not(inRange),

                FhirPrefix.LessThan =>
                    Expression.LessThan(date, startConst),

                FhirPrefix.GreaterThan =>
                    Expression.GreaterThanOrEqual(date, endConst),

                FhirPrefix.GreaterOrEqual =>
                    Expression.GreaterThanOrEqual(date, startConst),

                FhirPrefix.LessOrEqual =>
                    Expression.LessThan(date, endConst),

                FhirPrefix.StartAfter =>
                    Expression.GreaterThanOrEqual(date, endConst),

                FhirPrefix.EndBefore =>
                    Expression.LessThan(date, startConst),

                FhirPrefix.Approximate =>
                    BuildApproximateMatchExpression(date, start, end),

                _ => throw new ArgumentOutOfRangeException(nameof(prefix)),
            };

            return Expression.Lambda<Func<T, bool>>(match, parameter);
        }

        private static Expression BuildApproximateMatchExpression(
            Expression date,
            DateTime start,
            DateTime end)
        {
            const int padPercent = 10;

            var width = end - start;
            if (width <= TimeSpan.Zero)
            {
                width = TimeSpan.FromTicks(1);
            }

            var pad = TimeSpan.FromTicks(
                Math.Max(width.Ticks / padPercent,
                TimeSpan.FromDays(1).Ticks));

            var startConst = Expression.Constant(start - pad, typeof(DateTime));
            var endConst = Expression.Constant(end + pad, typeof(DateTime));
            return InRange(date, startConst, endConst);
        }

        private static Expression InRange(
            Expression date,
            ConstantExpression startConst,
            ConstantExpression endConst)
        {
            return Expression.AndAlso(
                Expression.LessThanOrEqual(startConst, date),
                Expression.LessThan(date, endConst));
        }

        private static (FhirPrefix Prefix, DateTimeInterval Interval) Parse(
            string raw)
        {
            var match = ParamRegex.Match(raw.Trim());
            if (!match.Success)
            {
                throw new ArgumentException(
                    $"Invalid Birthdate search parameter: '{raw}'");
            }

            var prefixStr = match.Groups["prefix"].Value;
            var prefix = string.IsNullOrEmpty(prefixStr)
                ? FhirPrefix.Equal
                : EnumHelper.ParseByEnumMemberValue<FhirPrefix>(prefixStr);

            var dateStr = match.Groups["date"].Value;
            return (prefix, ToInterval(dateStr));
        }

        private static DateTimeInterval ToInterval(
            string dateStr)
        {
            const int yearLength = 4;
            const int monthLength = 7;
            const int dayLength = 10;

            if (dateStr.Length == yearLength)
            {
                var year = int.Parse(dateStr, CultureInfo.InvariantCulture);
                var start = new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
                return new DateTimeInterval(start, start.AddYears(1));
            }
            if (dateStr.Length == monthLength && dateStr[yearLength] == '-')
            {
                const int monthStart = 5;
                var year = int.Parse(dateStr.AsSpan(0, yearLength), CultureInfo.InvariantCulture);
                var month = int.Parse(dateStr.AsSpan(monthStart, monthLength - monthStart), CultureInfo.InvariantCulture);
                var start = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Unspecified);
                return new DateTimeInterval(start, start.AddMonths(1));
            }
            if (dateStr.Length == dayLength && dateStr[yearLength] == '-' && dateStr[monthLength] == '-')
            {
                var start = DateTime.ParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                return new DateTimeInterval(start, start.AddDays(1));
            }
            if (dateStr.Contains('T', StringComparison.Ordinal))
            {
                const int timeLength = 19;
                const int colonPosition = 16;

                var hasSeconds = dateStr.Length >= timeLength &&
                    dateStr[colonPosition] == ':';
                var start = DateTime.ParseExact(
                    dateStr,
                    hasSeconds ? "yyyy-MM-ddTHH:mm:ss" : "yyyy-MM-ddTHH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None);
                var end = hasSeconds
                    ? start.AddSeconds(1)
                    : start.AddMinutes(1);
                return new DateTimeInterval(start, end);
            }

            throw new ArgumentException(
                $"Unrecognized FHIR date format: '{dateStr}'");
        }
    }
}
