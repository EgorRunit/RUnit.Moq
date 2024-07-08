using System.ComponentModel;

namespace BUnit.Interfaces
{
    /// <summary>
    /// Implements the fluent API.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReturnsThrowsGetter<TMock, TProperty> : IReturnsGetter<TMock, TProperty>, IThrows, IFluentInterface
        where TMock : class
    {
    }
}
