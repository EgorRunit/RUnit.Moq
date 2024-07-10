using System;
using System.Linq.Expressions;
using BUnit.Moq.Setups;

namespace BUnit.Moq
{
    /// <summary>
    /// Класс настройки поведения тестируемого типа.
    /// </summary>
    /// <typeparam name="T">Тестируемый тип.</typeparam>
    public class Mock<TMock> where TMock : class
    {
        ProxyMock<TMock> _proxyMock;

        public TMock Object
        {
            get
            {
                return _proxyMock as TMock;
            }
        }

        public Mock()
        {
            _proxyMock = TypeFactory.CreateProxy<TMock>();
        }

        public CallbackSetup<TMock> Setup(Expression<Action<TMock>> expression)
        {
            return new CallbackSetup<TMock>();
        }

        public ReturnsSetup<TMock, TReturnValue> Setup<TReturnValue>(Expression<Func<TMock, TReturnValue>> expression)
        {
            return new ReturnsSetup<TMock, TReturnValue>();
        }
    }
}
