using System.Linq.Expressions;

namespace BUnit.Test.Extensions
{
    public static class ExpressionExtension
    {
        public static Expression GetExpression<T>(this Expression<Action<T>> expression, int argumentIndex)
        {
            var lamdaExpression = expression as LambdaExpression;
            var methodCallExpression = lamdaExpression.Body as MethodCallExpression;
            return methodCallExpression.Arguments[0];
        }
    }
}
