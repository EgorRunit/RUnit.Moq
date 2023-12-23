using System.Reflection.Emit;

namespace RUnIt.Moq.Builders
{
    public interface IMockTypeBuilder<T> where T: class
    {
        Type Build(Mock<T> mock, ModuleBuilder moduleBuilder, IMockMethodBuilder<T> mockMethodBuilder);
    }
}
