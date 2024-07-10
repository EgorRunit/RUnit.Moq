using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConsole2.Interfaces;

namespace TestConsole2.Setups
{
    public interface IReturnsFunction<TReturnValue>
    {
        ReturnsResult<TReturnValue> Returns<TResult>(Func<TResult> valueFunction);
    }





    public class ReturnsFunction<TMock, TReturnValue> :
        CallbackFunctions<ReturnsThrow<TMock, TReturnValue>>,
        ISetup<TMock, TReturnValue>
    {
        public ReturnsFunction()
        {
        }
        public ReturnsResult<TReturnValue> Returns(Func<TReturnValue> valueFunction)
        {
            var value = valueFunction.Invoke();
            return new ReturnsResult<TReturnValue>(value);

        }
    }
}
