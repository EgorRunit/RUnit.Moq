using BUnit.Moq.Builders;
using BUnit.Moq.Setups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BUnit.Moq
{
    public interface IProxyMock
    {
        //ProxyMock<T> GetProxyMock();
    }

    public class ProxyMock
    {
        public CallbackManager callbackManager;

        public ProxyMock()
        {

        }

        public object Execute(string methodSignature, List<object> list, ProxyMock proxyMock)
        {
            var setupSetting = proxyMock.callbackManager.TryGetSetupSetting(methodSignature, list);
            if(setupSetting != null)
            {
                if (setupSetting is SetupSettingAction)
                {
                    (setupSetting as SetupSettingAction).ExecuteCallback(list);
                    return typeof(void);
                }
                else
                {
                    var setupSettingFunction = setupSetting as SetupSettingFunction;
                    setupSettingFunction.ExecuteCallback(list);
                    return setupSettingFunction.ExecuteReturn();
                }
            }
            return 666;
        }


        internal void RegisterCallbackManager(CallbackManager callbackManager)
        {
            this.callbackManager = callbackManager;
        }

    }



    /// <summary>
    /// Двойник тестируемого типа.
    /// </summary>
    /// <typeparam name="T">Тестируемы тип.</typeparam>
    public class ProxyMock<T> : ProxyMock  where T : class
    {
    }
}
