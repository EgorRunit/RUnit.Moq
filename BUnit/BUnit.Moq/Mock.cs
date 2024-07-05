using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BUnit.Moq.Setups;

namespace BUnit.Moq
{
    /// <summary>
    /// Класс настройки поведения тестируемого типа
    /// </summary>
    /// <typeparam name="T">Тустируемый тип</typeparam>
    public class Mock<T> where T : class
    {
        SetupSettings _mockSetupSettings;
        /// <summary>
        /// Экземпляр двойника тестируемого типа
        /// </summary>
        ProxyMock<T> _proxyMock;


        /// <summary>
        /// Экземпляр двойника тестируемого типа
        /// </summary>
        public T Object
        {
            get
            {
                return _proxyMock as T;
            }
        }

        /// <summary>
        /// Коснтруктор
        /// </summary>
        public Mock()
        {
            _mockSetupSettings= new SetupSettings();
            _proxyMock = TypeFactory.CreateProxy<T>();
            _proxyMock.RegisterCallbackManager(new CallbackManager(_mockSetupSettings));
        }


        public ActionSetup<T> Setup(Expression<Action<T>> expression)
        {
            var actionSetup = new ActionSetup<T>(expression);
            var mockSetupSetting = _mockSetupSettings.RegisterSetup(expression, actionSetup);
            return actionSetup;
        }

        //public FunctionSetup Setup(Expression<Func<T>> expression)
        //{
        //    //var methodCallExpression = expression.Body as MethodCallExpression;
        //    //Callback(methodCallExpression);
        //    //var setup = new Setup(methodCallExpression);
        //    //_proxyMock.AddCallback(setup);
        //    //return setup;
        //    return null;
        //}
    }
}
