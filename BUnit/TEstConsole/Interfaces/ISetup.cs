using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEstConsole.Interfaces
{
    public interface ISetup<T> :
        ICallback<ICallbackResult>
        where T : class
    {
    }

    public interface ISetup<T, TResult> :
        ICallback<IReturns<T, TResult>>,
        IReturns<T,TResult>
        where T : class
    { 
    }
}
