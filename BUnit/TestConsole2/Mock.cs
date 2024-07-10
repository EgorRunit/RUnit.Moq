using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestConsole2.Interfaces;
using TestConsole2.Setups;

namespace TestConsole2
{
    public class Mock<TMock>
    {
        TMock _tmock;

        public Mock(TMock mock)
        {
            _tmock = mock;
        }

        public CallbackSetup<TMock> Setup(Expression<Action<TMock>> expression)
        {
            return new CallbackSetup<TMock>();
        }

        public ReturnsFunction<TMock, TReturnValue> Setup<TReturnValue>(Expression<Func<TMock, TReturnValue>> expression)
        {
            return new ReturnsFunction<TMock, TReturnValue>();
        }
    }
}
