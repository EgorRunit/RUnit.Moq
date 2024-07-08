namespace BUnit.Interfaces
{
    public interface IReturnsResult<TMock> : ICallback, IRaise<TMock>, IVerifies, IFluentInterface
    {
    }
}
