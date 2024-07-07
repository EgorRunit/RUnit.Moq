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

        public void Execute(string methodSignature, List<object> list, ProxyMock proxyMock1)
        {
            var proxyMock = list.Last() as ProxyMock;
            var setupSetting = proxyMock.callbackManager.TryGetSetupSetting(methodSignature, list.GetRange(0, list.Count - 1));
            if(setupSetting != null)
            {
                if (setupSetting is SetupSettingAction)
                {
                    (setupSetting as SetupSettingAction).Execute(list.GetRange(0, list.Count - 1));
                    Console.WriteLine("SetupSettingAction");
                    var d2 = 4;
                }
                else
                {
                    Console.WriteLine("SetupSettingFunction");
                    var d2 = 4;
                }
            }

        }


        public void RegisterCallbackManager(CallbackManager callbackManager)
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
        //readonly CallbackManager _callbackManager;
        string ddddddddd = "rrrrrrrrrrrrrrrrrr";
        public ProxyMock()
        {
            //Object =
            //
            //TypeFactory.Get<T>(this) as InterfaceClass<T>;
        }

        void _Ddddd(string d, List<object> list)
        {
            callbackManager.TryGetSetupSetting("", null);
        }

        /// <summary>
        /// Вызов данного метода вставляется во все созданные метода двойника.
        /// </summary>
        /// <param name="methodSignature">Системная сигнатура вызываемого метода двойника.</param>
        /// <param name="list">Список параметров вызываемого метода двойника.</param>
        public  void Execute1(string methodSignature, List<object> list)
        {
            var proxyMock = list.Last() as ProxyMock;
            Console.WriteLine(methodSignature);
            if (list != null)
            {
                foreach (var item in list)
                {
                    Console.WriteLine($"{item}={item.GetType()}");
                }
            }
            if (proxyMock != null)
            {
                var parameters = new List<object>();
                for (var i = 0; i < list.Count - 1; i++)
                {
                    parameters.Add(list[i]);
                }
                _Ddddd(methodSignature, list);
                var setupSetting = callbackManager.TryGetSetupSetting(methodSignature, list.GetRange(0, list.Count - 1));
                if(setupSetting != null)
                {
                    if(setupSetting is SetupSettingAction)
                    {
                        Console.WriteLine("SetupSettingAction");
                        var d2 = 4;
                    }
                    else
                    {
                        Console.WriteLine("SetupSettingFunction");
                        var d2 = 4;
                    }
                }
                //var parameters = lit.ToSetupParameterList();
                //var parameters = list.GetRange(0, list.Count - 1).Select(x => x as ParameterInfo);
                //Console.WriteLine($"list count = {parameters.Count}");
                //proxyMock.callbackManager.CallbackIfExists(parameters);
                //list.Remove(proxyMock);
            }
        }
    }
}
