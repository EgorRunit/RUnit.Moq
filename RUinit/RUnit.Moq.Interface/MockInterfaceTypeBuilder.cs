using System.Reflection.Emit;
using System.Reflection;

namespace RUnit.Moq.Interface
{
    public class MockInterfaceTypeBuilder
    {
        //public T Build<T>(ModuleBuilder moduleBuilder, IMockMethodBuilder<T> mockMethodBuilder)
        //{
        //    var type = typeof(T);
        //    var typeBuilder = moduleBuilder.DefineType($"{type.FullName}UnitTest", TypeAttributes.Public | TypeAttributes.Class, typeof(ProxyMock<T>));
        //    typeBuilder.AddInterfaceImplementation(typeof(T));



        //    var methods = type.GetMethods();
        //    foreach (var method in methods)
        //    {
        //        mockMethodBuilder.Build(mock, typeBuilder, method);
        //    }
        //    var createdType = typeBuilder.CreateType();
        //    if (createdType == null)
        //    {
        //        throw new Exception();
        //    }
        //    return createdType;
        //}
    }
}
