using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace BUnit.Moq.Builders
{
    public class InterfaceClass<T>
    {

    }

    public class InterfaceTypeBuilder : IInterfaceTypeBuilder
    {
        public Type Build<T>(ModuleBuilder moduleBuilder, InterfaceMethodBuilder<T> mockMethodBuilder) where T : class
        {
            var type = typeof(T);
            //var typeBuilder = moduleBuilder.DefineType($"{type.FullName}UnitTest",   TypeAttributes.Public | TypeAttributes.Class, typeof(ProxyMock<T>));
            var typeBuilder = moduleBuilder.DefineType($"{type.FullName}UnitTest", TypeAttributes.Public | TypeAttributes.Class, typeof(ProxyMock<T>));

            typeBuilder.AddInterfaceImplementation(typeof(T));


            var genericType = typeof(ProxyMock<>).MakeGenericType(typeof(T));
            var methods = type.GetMethods();
            foreach (var method in methods)
            {
                mockMethodBuilder.Build(typeBuilder, method, genericType);
            }
            var createdType = typeBuilder.CreateTypeInfo();

            if (createdType == null)
            {
                throw new Exception();
            }
            var ee = createdType as ProxyMock<T>;
            return createdType;
        }
    }
}
