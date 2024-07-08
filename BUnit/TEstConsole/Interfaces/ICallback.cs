using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEstConsole.Interfaces
{
    public interface ICallback<TReuslt>
    {
        TReuslt Callback(Action action);
        TReuslt Callback<T1>(Action<T1> action);
        TReuslt Callback<T1, T2>(Action<T1, T2> action);
    }
}
