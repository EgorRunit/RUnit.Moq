using System.Reflection.Emit;
using System.Reflection;

namespace RUnIt.Moq.Builders
{
    public interface IMockMethodBuilder<T> where T : class
    {
        void Build(Mock<T> mock, TypeBuilder typeBuilder, MethodInfo methodInfo);
    }
}
