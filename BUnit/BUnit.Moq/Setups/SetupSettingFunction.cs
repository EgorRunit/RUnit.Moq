using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BUnit.Moq.Setups
{
    public class SetupSettingFunction : SetupSetting
    {
        protected Delegate callback;
        protected Func<object> returns;

        internal SetupSettingFunction(LambdaExpression lambdaExpression) : base(lambdaExpression)
        {
        }

        public SetupSettingFunction Callback(Action action)
        {
            callback = action;
            return this;
        }

        public SetupSettingFunction Callback<T1>(Action<T1> action)
        {
            callback = action;
            return this;
        }
        public SetupSettingFunction Callback<T1, T2>(Action<T1, T2> action)
        {
            callback = action;
            return this;
        }

        public SetupSettingFunction Callback<T1, T2, T3>(Action<T1, T2, T3> action)
        {
            callback = action;
            return this;
        }

        public void Execute(List<object> methodParameters)
        {
            callback.DynamicInvoke(methodParameters.ToArray());
        }


        public SetupSettingFunction Return<TRetun>(TRetun x)
        {
            returns = () => x;
            return this;
        }
    }
}
