using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUnIt.Moq.Console.Interfaces
{
    public interface ICallBase : IFluentInterface
    {
        /// <summary>
        /// Calls the real method of the object.
        /// </summary>
        ICallBaseResult CallBase();
    }
}
