namespace BUnit.Interfaces
{ 
    public interface IReturnsThrows<TMock, TResult> : IReturns<TMock, TResult>, IThrows, IFluentInterface where TMock : class
    {
    }
}
