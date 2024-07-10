using System.Linq.Expressions;

namespace TestConsole2.Interfaces
{
    public interface ISetup<TMock>
    {
    }
    public interface ISetup<TMock, TReturnValue> : ISetup<TMock>
    {
    }
}
