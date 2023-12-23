using System;
using System.Linq.Expressions;
using RUnIt.Moq.Builders;

namespace RUnIt.Moq
{
    public class Mock<T> where T : class 
    {
        ProxyMock<T>? _proxyMock;
        Dictionary<string, Setup> _actions;
        
        public T Object { get; private set; }

        public Mock()
        {
            _actions= new Dictionary<string, Setup>();
            var type =TypeFactory.Get<T>(this);
            var createdType = Activator.CreateInstance(type) as T;
            if (createdType != null)
            {
                Object = createdType;
            }
            else
            {
                throw new Exception("Не удалось создать тип");
            }
        }

        public Setup Setup(Expression<Action<T>> expression)
        {
            //var sss = new System.Linq.Expressions.Expression.MethodCallExpressionProxy(new System.Linq.Expressions.Expression.LambdaExpressionProxy(expression).Body).Arguments
            var me = expression as MethodCallExpression;
            var methodCallExp = (MethodCallExpression)expression.Body;
            var arguments = methodCallExp.Arguments;
            var sgfdf = arguments[1].ToString();
            //var ddd = ((RuntimeMethodInfo)(new System.Linq.Expressions.Expression.MethodCallExpressionProxy(arguments[1]).Method)).Name;

            if (me != null)
            {
                var ff = me.Arguments;
            }
            //var dd = expression.Body
            var type = Object.GetType();

            

           var expresionName = expression.Body.ToString();
            var setup = new Setup();
            if(_actions.ContainsKey(expresionName))
            {
                _actions[expresionName] = setup;
            }
            else
            {
                _actions.Add(expresionName, setup);
            }
            return setup;
        }
    }
}
