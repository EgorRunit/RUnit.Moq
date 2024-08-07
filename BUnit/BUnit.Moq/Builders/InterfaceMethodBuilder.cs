using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Common;

namespace BUnit.Moq.Builders
{
    public class InterfaceMethodBuilder<T> : IInterfaceMethodBuilder<T> where T : class
    {
        public void Build(TypeBuilder typeBuilder, MethodInfo methodInfo)
        {
            var methodBuilder = typeBuilder.DefineMethod(
                methodInfo.Name,
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.CheckAccessOnOverride,
                methodInfo.ReturnType,
                methodInfo.GetParameters().Select(x => x.ParameterType).ToArray());

            

            //typeBuilder.GetMethod("Execute", BindingFlags.Public | BindingFlags.CreateInstance);
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

        void _generateEntryParameters( ILGenerator ilGenerator, MethodInfo methodInfo)
        {
            var executeParameterTypes = new Type[] { typeof(string), typeof(List<object>) };
            var executeMethodInfo = typeof(ProxyMock<T>).GetMethod("Execute", BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);



            if (executeMethodInfo == null)
            {
                throw new Exception();
            }

            var listConstrutorInfo = typeof(List<object>).GetConstructor(Type.EmptyTypes);
            var listAddMethodInfo = typeof(List<object>).GetMethod("Add", BindingFlags.Instance | BindingFlags.Public);
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
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Call, listAddMethodInfo);

            ilGenerator.Emit(OpCodes.Ldloc_0);                  //Загружаем аргументы функции Execute
            ilGenerator.Emit(OpCodes.Ldstr, methodInfo.ToString());   //Название вызываемой функции

            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.Emit(OpCodes.Call, executeMethodInfo);
        }
    }
}
