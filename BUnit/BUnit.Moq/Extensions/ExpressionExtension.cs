using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq.Expressions
{
    public static class ExpressionExtension
    {
        public static string GetMethodSignature<T>(this Expression<Action<T>> expression)
        {
            var methodCallExpression = expression.Body as MethodCallExpression;
            var method = methodCallExpression.Method;
            var methodSignature = method.ToString();
            return methodSignature;
        }

        public static string GetMethodSignature(this Expression<Action> expression)
        {
            var methodCallExpression = expression.Body as MethodCallExpression;
            var method = methodCallExpression.Method;
            var methodSignature = method.ToString();
            return methodSignature;
        }
    }
}
