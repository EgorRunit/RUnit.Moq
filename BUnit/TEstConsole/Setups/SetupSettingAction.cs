using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEstConsole.Interfaces;
using Void = TEstConsole.Interfaces;

namespace TEstConsole.Setups
{
    public class SetupSettingAction<T> : CallbackBase<ICallbackResult>, ISetup<T> where T : class
    {
        public SetupSettingAction()
        {
            
        }
    }
}
