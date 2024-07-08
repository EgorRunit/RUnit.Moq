using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEstConsole.Interfaces;

namespace TEstConsole.Setups
{
    public class BaseReturns<T> : CallbackBase<IReturnsResult<T>>, IReturns where T : class
    {

        public IReturnsResult Returns<TResult>()
        {
           
            throw new NotImplementedException();
        }

        public IReturnsResult Returns<T1, TResult>(T1 t1)
        {
            Console.WriteLine($"Returns<T1, TResult>(T1 t1) = {t1}");
            return null;
        }

        public IReturnsResult Returns<T1, T2, TResult>(T1 t1, T2 t2)
        {
            throw new NotImplementedException();
        }
    }
}
