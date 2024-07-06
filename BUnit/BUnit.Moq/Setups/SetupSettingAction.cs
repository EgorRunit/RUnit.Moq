using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BUnit.Moq.Setups
{
    public class SetupSettingAction : SetupSetting
    {
        /// <summary>
        /// Сслыка на обрабную функция вызов
        /// </summary>
        protected Delegate callback;

        internal SetupSettingAction(LambdaExpression lambdaExpression) : base(lambdaExpression)
        {
        }

        public void Callback(Action action)
        {
//            callback = () => action.DynamicInvoke(buildDynamicCallback());
        }

        public void Callback<T1>(Action<T1> action)
        {
//            callback = () => action.DynamicInvoke(buildDynamicCallback());
        }
        public void Callback<T1, T2>(Action<T1, T2> action)
        {
//            callback = () => action.DynamicInvoke(buildDynamicCallback());
        }

        public void Callback<T1, T2, T3>(Action<T1, T2, T3> action)
        {
            callback = action;
        }

        public void Execute(List<object> methodParameters)
        {
            callback.DynamicInvoke(methodParameters.ToArray());
        }

    }
}
