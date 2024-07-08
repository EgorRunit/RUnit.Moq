
using System;

namespace BUnit.Interfaces
{
    public interface ICallbackSetter<TProperty> : IFluentInterface
    {
        ICallbackResult Callback(Action<TProperty> action);
    }
}
