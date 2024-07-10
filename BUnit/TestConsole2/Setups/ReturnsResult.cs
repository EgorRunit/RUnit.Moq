using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole2.Setups
{
    public class ReturnsResult<TReturnValue>
    {
        TReturnValue _value;
        public ReturnsResult(TReturnValue value)
        {
            _value= value;
        }
    }
}
