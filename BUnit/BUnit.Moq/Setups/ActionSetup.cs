using System;
using System.Linq.Expressions;

namespace BUnit.Moq.Setups
{
    internal class ActionSetup<T> : ActionSetupBase
    {

        Expression<Action<T>> _expression;
        protected CallbackManager callbackManager;

        public ActionSetup(Expression<Action<T>> expression)
        {
            _expression = expression;
            methodCallExpression = (_expression.Body as MethodCallExpression);
            MethodCallSignature = expression.ToString();
            MethodCallSignature = MethodCallSignature.Substring(MethodCallSignature.IndexOf('.') + 1);
            MethodSignature = expression.GetMethodSignature();
        }

        public void Callback(Action action)
        {
            callback = () => action.DynamicInvoke(buildDynamicCallback());
        }

        public void Callback<T1>(Action<T1> action)
        {
            callback = () => action.DynamicInvoke(buildDynamicCallback());
        }
        public void Callback<T1, T2>(Action<T1, T2> action)
        {
            callback = () => action.DynamicInvoke(buildDynamicCallback());
        }
    }
}
