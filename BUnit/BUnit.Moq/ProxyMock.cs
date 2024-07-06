using BUnit.Moq.Builders;
using BUnit.Moq.Setups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
        readonly internal CallbackManager _callbackManager;

        public ProxyMock()
        {
            //Object = TypeFactory.Get<T>(this) as InterfaceClass<T>;
        }

        /// <summary>
        /// Вызов данного метода вставляется во все созданные метода двойника.
        /// </summary>
        /// <param name="methodSignature">Системная сигнатура вызываемого метода двойника.</param>
        /// <param name="list">Список параметров вызываемого метода двойника.</param>
        public void Execute(string methodSignature, List<object> list)
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
                var parameters = list.ToSetupParameterList();
                //var parameters = list.GetRange(0, list.Count - 1).Select(x => x as ParameterInfo);
                //Console.WriteLine($"list count = {parameters.Count}");
                //proxyMock.callbackManager.CallbackIfExists(parameters);
                //list.Remove(proxyMock);
            }
        }
    }
}
