using System.Reflection.Emit;
using System.Reflection;

namespace RUnIt.Moq.Builders
{
    internal class MockMethodBuilder<T> : IMockMethodBuilder<T> where T : class
    {
        void IMockMethodBuilder<T>.Build(Mock<T> mock,TypeBuilder typeBuilder, MethodInfo methodInfo)
        {
            var methodBuilder = typeBuilder.DefineMethod(
                methodInfo.Name,
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.CheckAccessOnOverride,
                methodInfo.ReturnType,
                methodInfo.GetParameters().Select(x => x.ParameterType).ToArray());

            var ilGenerator = methodBuilder.GetILGenerator();
            _generateEntryParameters(ilGenerator, methodInfo);

            if (methodInfo.ReturnType.Name != "Void")
            {
                var retValue = ilGenerator.DeclareLocal(methodInfo.ReturnType);
                ilGenerator.Emit(OpCodes.Ldloc_0, retValue);
                ilGenerator.Emit(OpCodes.Ret);
            }
            else
            {
                ilGenerator.Emit(OpCodes.Ret);
            }
        }

        void _generateEntryParameters(ILGenerator ilGenerator, MethodInfo methodInfo)
        {
            var executeParameterTypes = new Type[] { typeof(string), typeof(List<object>) };
            var executeMethodInfo = typeof(ProxyMock<T>).GetMethod("Execute", BindingFlags.Instance  | BindingFlags.NonPublic, executeParameterTypes);
            if (executeMethodInfo == null)
            {
                throw new Exception();
            }
            var listConstrutorInfo = typeof(List<Object>).GetConstructor(Type.EmptyTypes);
            var listAddMethodInfo = typeof(List<Object>).GetMethod("Add", BindingFlags.Instance | BindingFlags.Public, new Type[] { typeof(Object) });
            var methodInfoParameters = methodInfo.GetParameters();

            //создаем локальную переменную типа List<object>
            var localList = ilGenerator.DeclareLocal(typeof(List<Object>));
            ilGenerator.Emit(OpCodes.Newobj, listConstrutorInfo);
            ilGenerator.Emit(OpCodes.Stloc, localList);

            //Заполняем список значениями аргументов
            for (var i = 0; i < methodInfoParameters.Length; i++)
            {
                var parameter = methodInfoParameters[i];
                if (parameter.ParameterType.IsValueType)
                {
                    ilGenerator.Emit(OpCodes.Ldloc_0);
                    ilGenerator.Emit(OpCodes.Ldarg, i + 1);
                    ilGenerator.Emit(OpCodes.Box, parameter.ParameterType);
                    ilGenerator.Emit(OpCodes.Callvirt, listAddMethodInfo);
                }
                else
                {
                    ilGenerator.Emit(OpCodes.Ldloc_0);
                    ilGenerator.Emit(OpCodes.Ldarg, i + 1);
                    ilGenerator.Emit(OpCodes.Callvirt, listAddMethodInfo);
                }
            }

            ilGenerator.Emit(OpCodes.Ldloc_0);                  //Загружаем аргументы функции Execute
            ilGenerator.Emit(OpCodes.Ldstr, methodInfo.Name);   //Название вызываемой функции
            ilGenerator.Emit(OpCodes.Ldloc_0);                  //Список аргументов-значений функции
            ilGenerator.Emit(OpCodes.Call, executeMethodInfo);
        }
    }
}
