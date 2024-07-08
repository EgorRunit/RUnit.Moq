using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEstConsole.Interfaces
{
    public interface IReturns<TResult>
    {
        TResult Returns<TResult>();
        TResult Returns<T1, TResult>(T1 t1);
        TResult Returns<T1, T2, TResult>(T1 t1, T2 t2);
    }
}
