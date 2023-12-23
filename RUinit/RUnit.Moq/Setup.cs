using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUnIt.Moq
{
    public class Setup
    {
        Action _action;

        internal Setup() 
        { 
        }

        public void Execute(Action action) 
        { 
            _action = action;
        }
    }
}
