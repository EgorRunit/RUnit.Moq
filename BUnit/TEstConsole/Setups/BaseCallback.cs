using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEstConsole.Interfaces;
using v = TEstConsole.Interfaces;

namespace TEstConsole.Setups
{
    public class CallbackBase<TResult> : ICallback<TResult> where TResult : class
    {
        public TResult Callback(Action action)
        {
            throw new NotImplementedException();
        }

        public TResult Callback<T1>(Action<T1> action)
        {
            Console.WriteLine($"Callback<T1>(Action<T1> action) = {action}");
            return null;
        }

        public TResult Callback<T1, T2>(Action<T1, T2> action)
        {
            Console.WriteLine($"Callback<T1, T2>(Action<T1, T2> action) = {action}");
            return null;
        }
    }

    public class CallbackBase<TMock, TResult> : CallbackBase<TMock> where TMock : class
    {

    }
}
