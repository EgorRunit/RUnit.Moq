using BUnit.Moq.Setups;
using System.Linq.Expressions;

namespace BUnit.Test.Extensions
{
    public static class ExpressionExtension
    {
        //public static List<SetupArgument> GetExpressions(this Expression<Action> expression)
        //{
        //    var lamdaExpression = expression as LambdaExpression;
        //    var methodCallExpression = lamdaExpression.Body as MethodCallExpression;
        //    return methodCallExpression.Arguments.ToSetupParameterList();
        //}

        //public static Expression GetExpression(this Expression<Action> expression, int argumentIndex)
        //{
        //    var lamdaExpression = expression as LambdaExpression;
        //    var methodCallExpression = lamdaExpression.Body as MethodCallExpression;
        //    return methodCallExpression.Arguments[0];
        //}
    }
}
