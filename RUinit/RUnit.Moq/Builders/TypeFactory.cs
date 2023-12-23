using System.Reflection.Emit;
using System.Reflection;

namespace RUnIt.Moq.Builders
{
    public static class TypeFactory
    {
        static Dictionary<Type, Type> _types = new Dictionary<Type, Type>();
        static AssemblyBuilder _assemblyBuilder;
        static ModuleBuilder _moduleBuilder;

        static TypeFactory()
        {
            var assemblyName = new AssemblyName("DynamicAssemblyUnitTest");
            _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            _moduleBuilder = _assemblyBuilder.DefineDynamicModule(assemblyName.Name ?? "DynamicAssemblyUnitTest");
        }

        public static Type Get<T>(Mock<T> mock) where T : class
        {
            var type = typeof(T);
            if(_types.ContainsKey(type))
            {
                return _types[type];
            }
            else
            {
                IMockTypeBuilder<T> typeBuilder = new MockTypeBuilder<T>();
                IMockMethodBuilder<T> methodBuilder = new MockMethodBuilder<T>();
                var createdType = typeBuilder.Build(mock, _moduleBuilder, methodBuilder);
                _types.Add(type, createdType);
            }
            return _types[type];
        }
    }
}
