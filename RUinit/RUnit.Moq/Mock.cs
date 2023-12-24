using System;
using System.Linq.Expressions;
using RUnIt.Moq.Abstracts;
using RUnIt.Moq.Builders;
using RUnIt.Moq.Interfaces;

namespace RUnIt.Moq
{
    public class Mock<T> : RUnIt.Moq.Abstracts.Mock where T : class 
    {
        ProxyMock<T>? _proxyMock;
        //Dictionary<string, Setup> _actions;
        
        public T Object { get; private set; }

        public Mock()
        {
            //_actions= new Dictionary<string, Setup>();
            var type =TypeFactory.Get<T>(this);
            var createdType = Activator.CreateInstance(type) as T;
            if (createdType != null)
            {
                Object = createdType;
                _proxyMock = Object as ProxyMock<T>;
            }
            else
            {
                throw new Exception("Не удалось создать тип");
            }
        }

        public ISetup Setup(Expression<Action<T>> expression)
        {
            var methodCallExpression = expression.Body as MethodCallExpression;
            Callback(methodCallExpression);
            var setup = new Setup(methodCallExpression);
            _proxyMock.AddCallback(setup);
            return setup;
        }

        //public Setup<T> Setup(Expression<Action<T>> expression)
        //{
        //    var methodCallExpression = expression.Body as MethodCallExpression;
        //    //_proxyMock.Execute(methodCallExpression);
        //    var setup = new Setup<T>(_proxyMock);
        //    return setup;

        //    //var sss = new System.Linq.Expressions.Expression.MethodCallExpressionProxy(new System.Linq.Expressions.Expression.LambdaExpressionProxy(expression).Body).Arguments
        //    //var methodCallExpression = (MethodCallExpression)expression.Body;




        //   // var arguments = methodCallExp.Arguments;
        //   // var meta = methodCallExp.Method.MetadataToken;
        //   // var sgfdf = arguments[1].ToString();
        //   // //var ddd = ((RuntimeMethodInfo)(new System.Linq.Expressions.Expression.MethodCallExpressionProxy(arguments[1]).Method)).Name;

        //   // if (me != null)
        //   // {
        //   //     var ff = me.Arguments;
        //   // }
        //   // //var dd = expression.Body
        //   // var type = Object.GetType();



        //   //var expresionName = expression.Body.ToString();
        //   // var setup = new Setup();
        //   // if(_actions.ContainsKey(expresionName))
        //   // {
        //   //     _actions[expresionName] = setup;
        //   // }
        //   // else
        //   // {
        //   //     _actions.Add(expresionName, setup);
        //   // }
        //}
    }
}
