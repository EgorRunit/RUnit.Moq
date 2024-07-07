using System.Reflection.Emit;
using System.Reflection;
using System;

namespace BUnit.Moq.Builders
{
    /// <summary>
    /// Интерфейс для класса генерирования метода для нового типа имплументирующего указанный интерфейс.
    /// </summary>
    internal interface IInterfaceMethodBuilder<T> where T : class
    {
        /// <summary>
        /// Сгенерировать новый метод для указанного создателя типов.
        /// </summary>
        /// <param name="typeBuilder"></param>
        /// <param name="methodInfo"></param>
        void Build(TypeBuilder typeBuilder, MethodInfo methodInfo, Type genericType);
    }
}
