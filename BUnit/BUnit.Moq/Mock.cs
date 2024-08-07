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
    public partial class Mock<T> where T : class
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


        public SetupSettingAction Setup(Expression<Action<T>> expression)
        {
            var setupSetting = _mockSetupSettings.RegisterSetupAction(expression);
            return setupSetting as SetupSettingAction;
        }

        public SetupSettingFunction Setup(Expression<Func<T>> expression)
        {
            var setupSetting = _mockSetupSettings.RegisterSetupFunction(expression);
            return setupSetting as SetupSettingFunction;
        }
    }
}
