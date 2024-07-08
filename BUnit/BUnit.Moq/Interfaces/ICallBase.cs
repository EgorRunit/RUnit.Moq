
namespace BUnit.Interfaces
{
    public interface ICallBase : IFluentInterface
    {
        /// <summary>
        /// Calls the real method of the object.
        /// </summary>
        ICallBaseResult CallBase();
    }
}
