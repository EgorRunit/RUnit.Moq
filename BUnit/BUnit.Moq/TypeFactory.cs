using BUnit.Moq.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Linq.Expressions;
using BUnit.Moq.Setups;

namespace BUnit.Moq
{
    /// <summary>
    /// Фабрика создания типов заглушек
    /// </summary>
    internal static class TypeFactory
    {
        /// <summary>
        /// Коллекйя ранее созданных типов
        /// </summary>
        static Dictionary<Type, Type> _types = new Dictionary<Type, Type>();
        /// <summary>
        /// Новая сборка типов заглущек
        /// </summary>
        static AssemblyBuilder _assemblyBuilder;
        /// <summary>
        /// Экземпляр создания нового модуля
        /// </summary>
        static ModuleBuilder _moduleBuilder;
        static InterfaceTypeBuilder _interfaceTypeBuilder;

        static TypeFactory()
        {
            var assemblyName = new AssemblyName("DynamicAssemblyUnitTest");
            _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            _moduleBuilder = _assemblyBuilder.DefineDynamicModule(assemblyName.Name ?? "DynamicAssemblyUnitTest");
            var ddd = _assemblyBuilder.GetReferencedAssemblies();
            var sss = _assemblyBuilder.GetLoadedModules();
            _interfaceTypeBuilder = new InterfaceTypeBuilder();
            //AppDomain.CurrentDomain.Load(assemblyName);
        }

        /// <summary>
        /// Получение указанного типа.
        /// </summary>
        /// <typeparam name="T">Требуемый тип.</typeparam>
        /// <returns>Требуемый тип.</returns>
        internal static ProxyMock<T> CreateProxy<T>() where T : class
        {
            var type = typeof(T);
            ProxyMock<T> proxydMock = null;
            Type proxyMockType = null;
            if (!_types.ContainsKey(type))
            {
                if (type.IsInterface)
                {
                    var interfaceMethodBuilder = new InterfaceMethodBuilder<T>();
                    proxyMockType = _interfaceTypeBuilder.Build(_moduleBuilder, interfaceMethodBuilder);
                    _types.Add(type, proxyMockType);
                }
            }
            proxyMockType = _types[type];
            proxydMock = Activator.CreateInstance(proxyMockType) as ProxyMock<T>;
            return proxydMock;
        }
    }
}
