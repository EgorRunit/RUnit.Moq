using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEstConsole.Interfaces
{
    public interface IReturns<T, TResult>
    {
        IReturnsResult<T> Returns(Func<TResult> valueFunction);
        IReturnsResult<T> Returns<T1>(Func<T1, TResult> valueFunction);
        IReturnsResult<T> Returns<T1, T2>(Func<T1, T2, TResult> valueFunction);
    }
}
