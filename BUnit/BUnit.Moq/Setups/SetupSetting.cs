using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BUnit.Moq.Setups
{
    /// <summary>
    /// Класс для обработки метода Mock.Setup().
    /// </summary>
    public class SetupSetting
    {
        readonly List<SetupArgument> _setupParameters;

        /// <summary>
        /// Количество в лямда выражения аргументов с любым значением.
        /// Чем их большеб тем меньшк ранг настройки выражения для Mock.Setup.
        /// </summary>
        readonly internal int AnyCount;
        /// <summary>
        /// Системная сигнатура метода
        /// </summary>
        readonly internal string MethodOriginalSignature;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="lambdaExpression">Экземпляр лямда выражения для Mock.Setup.</param>
        public SetupSetting(LambdaExpression lambdaExpression)
        {
            var methodCallExpression = lambdaExpression.Body as MethodCallExpression;
            var method = methodCallExpression.Method;
            MethodOriginalSignature = methodCallExpression.Method.ToString();
            _setupParameters = methodCallExpression.Arguments.ToSetupParameterList();
            AnyCount = _setupParameters.Where(x => x.SetupArgumentType == SetupArgumentType.AnyValue).Count();
        }

        public bool EqualsSetupParamaters(SetupSetting setupSetting)
        {
            return _setupParameters.SequenceEqual(setupSetting._setupParameters);
        }

        public bool EqualsSetupParametersToObjectList(List<object> objects)
        {
            return _setupParameters.SequenceEqual(objects);
        }
    }
}
