using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BUnit.Moq.Setups
{
    /// <summary>
    /// Класс предназначен для управления настройками
    /// переданными в метод Mock.Setup.
    /// </summary>
    public class SetupSettings
    {
        /// <summary>
        /// Справочник зарегистрированных настроек в методе Mock.Setup.
        /// </summary>
        Dictionary<string, List<SetupSetting>> _mockSetupSettings;

        /// <summary>
        /// Конструктор
        /// </summary>
        internal SetupSettings()
        {
            _mockSetupSettings = new Dictionary<string, List<SetupSetting>>();
        }

        internal SetupSettingAction RegisterSetupAction(LambdaExpression lambdaExpression)
        {
            var setupSetting = new SetupSettingAction(lambdaExpression);
            _register(setupSetting);
            return setupSetting;
        }

        internal SetupSettingFunction RegisterSetupFunction(LambdaExpression lambdaExpression)
        {
            var setupSetting = new SetupSettingFunction(lambdaExpression);
            _register(setupSetting);
            return setupSetting;
        }


        void _register(SetupSetting setupSetting)
        { 

            if (!_mockSetupSettings.ContainsKey(setupSetting.MethodOriginalSignature))
            {
                _mockSetupSettings.Add(setupSetting.MethodOriginalSignature, new List<SetupSetting>());
                _mockSetupSettings[setupSetting.MethodOriginalSignature].Add(setupSetting);
            }
            else
            {
                var list = _mockSetupSettings[setupSetting.MethodOriginalSignature];
                var oldSetupSetting = list.FirstOrDefault(x => x.EqualsSetupParamaters(setupSetting));
                if(oldSetupSetting != null)
                {
                    list.Remove(oldSetupSetting);
                }
                list.Add(setupSetting);
            }
        }

        /// <summary>
        /// Попытка получить SetupSetting настройки которого макимально
        /// совпадают со списком аргументов вызванного метода.
        /// Совпадающие настройки замещаются последней.
        /// </summary>
        /// <param name="methodOriginalSignature">Системное сигнатура вызываемого метода.</param>
        /// <param name="methodArguments">Список аргументов вызванного метода.</param>
        /// <returns>
        /// SetingSetting или null если подходящего шаблона
        /// настроект не было найдено.
        /// </returns>
        public SetupSetting TryGetSetupSetting(string methodOriginalSignature, List<object> methodArguments)
        {
            SetupSetting setupSetting = null;
            if(_mockSetupSettings.ContainsKey(methodOriginalSignature))
            {
                var setupSettings = _mockSetupSettings[methodOriginalSignature];
                var foundSetupSettings = setupSettings.Where(x => x.EqualsSetupParametersToObjectList(methodArguments)).ToList();
                setupSetting = foundSetupSettings.OrderBy(x => x.AnyCount).FirstOrDefault();
            }
            return setupSetting;
                
        }
    }
}
