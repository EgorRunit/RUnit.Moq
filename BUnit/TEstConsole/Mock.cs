using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TEstConsole.Interfaces;
using TEstConsole.Setups;

namespace TEstConsole
{
    public class Mock<T> where T : class
    {
        public ISetup<T> Setup(Expression<Action<T>> expression)
        {
            var setupSettingAction = new SetupSettingAction<T>();
            return setupSettingAction;
        }

        public ISetup<T, TResult> Setup<TResult>(Expression<Func<T, TResult>> expression)
        {
            var setupSettingFunction = new SetupSettingFunction<T, TResult>();
            return setupSettingFunction;
        }
    }
}
