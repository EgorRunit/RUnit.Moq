using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEstConsole.Interfaces;

namespace TEstConsole.Setups
{
    public class SetupSettingFunction<T, TResult> :
        CallbackBase<IReturns<T, TResult>>,
        ISetup<T,TResult>
        where T : class
    {
        public SetupSettingFunction()
        {
            
        }
    }
}


