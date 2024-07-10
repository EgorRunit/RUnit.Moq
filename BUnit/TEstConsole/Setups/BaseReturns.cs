using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEstConsole.Interfaces;

namespace TEstConsole.Setups
{
    public class BaseReturns<T, TResult> :
        CallbackBase<IReturnsResult<T>>,
        IReturns<T, TResult>
        where T : class
    {

        public IReturnsResult<T> Returns(Func<TResult> valueFunction)
        {
           
            throw new NotImplementedException();
        }

        public IReturnsResult<T> Returns<T1>(Func<T1, TResult> valueFunction)
        {
            //this.r
            Console.WriteLine($"Returns<T1, TResult>(T1 t1) = {valueFunction}");
            return null;
        }

        public IReturnsResult<T> Returns<T1, T2>(Func<T1, T2, TResult> valueFunction)
        {
            throw new NotImplementedException();
        }
    }
}
