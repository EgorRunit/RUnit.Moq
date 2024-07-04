using BUnit.Moq.Setups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BUnit.Test.ClassHelpers
{
    public class ExpressionHelper<T>
    {
        public MethodCallExpression Setup(Expression<Action<T>> expression)
        {
            var lamdaExpression = expression as LambdaExpression;
            var methodCallExpression = lamdaExpression.Body as MethodCallExpression;
            return methodCallExpression;
        }
    }
}
