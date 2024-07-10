using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole2.Setups
{
    public class CallbackFunctions<TResult>
    {
        TResult _callbackResult;
        protected Delegate callback;


        public CallbackFunctions()
        {
            _callbackResult = Activator.CreateInstance<TResult>();
        }

        public TResult Callback(Action action)
        {
            callback = action;
            Console.WriteLine($"Callback<TResult>(Action action) = {action} Type = {GetType()}");
            return _callbackResult;
        }

        public TResult Callback<T1>(Action<T1> action)
        {
            callback = action;
            Console.WriteLine($"Callback<TResult>(Action<T1> action) = {action} Type = {GetType()}");
            return _callbackResult;
        }

        public void Execute(List<object> methodParameters)
        {
            callback.DynamicInvoke(methodParameters.ToArray());
        }


    }
}
