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
            var type1 = typeof(TResult);
            var type2 = typeof(T1);
            var type3 = typeof(T2);
            var dddd = new CallbackBase<IReturns<TResult, TResult>>();
            var dddd1 = dddd as TResult;
            var result = new CallbackResult<TResult>();
            var dss = result as TResult;
            //var sss = result as TResult;
            return dss;
        }
    }

    public class CallbackBase<TMock, TResult> : CallbackBase<TMock> where TMock : class
    {

    }
}
