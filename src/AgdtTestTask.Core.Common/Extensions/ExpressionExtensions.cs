using System.Linq.Expressions;

namespace AgdtTestTask.Core.Common.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var visitor = new ReplaceParameterVisitor(
                expr2.Parameters[0],
                expr1.Parameters[0]);

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    expr1.Body,
                    visitor.Visit(expr2.Body)),
                expr1.Parameters);
        }

        public static Expression<Func<T, bool>> CombineAnd<T>(
            this IEnumerable<Expression<Func<T, bool>>> expressions)
        {
            using var en = expressions.GetEnumerator();
            if (!en.MoveNext())
            {
                throw new InvalidOperationException();
            }

            var param = Expression.Parameter(typeof(T), "e");
            var body = new ReplaceParameterVisitor(
                en.Current.Parameters[0], param)
                    .Visit(en.Current.Body);
            while (en.MoveNext())
            {
                var next = new ReplaceParameterVisitor(
                    en.Current.Parameters[0], param)
                        .Visit(en.Current.Body);
                body = Expression.AndAlso(body, next);
            }

            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        private class ReplaceParameterVisitor(
            ParameterExpression from,
            ParameterExpression to)
            : ExpressionVisitor
        {
            protected override Expression VisitParameter(
                ParameterExpression node)
            {
                if (ReferenceEquals(node, from))
                {
                    return to;
                }

                return base.VisitParameter(node);
            }
        }
    }
}
