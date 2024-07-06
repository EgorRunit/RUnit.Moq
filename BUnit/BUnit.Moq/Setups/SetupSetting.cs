using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BUnit.Moq.Setups
{
    /// <summary>
    /// Класс для обработки метода Mock.Setup().
    /// </summary>
    public abstract class SetupSetting
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

        protected object[] buildDynamicCallback()
        {
            //var paramameters = new object[methodCallExpression.Arguments.Count];
            //for (var i = 0; i < methodCallExpression.Arguments.Count; i++)
            //{
            //    var arg = methodCallExpression.Arguments[i];
            //    switch (arg.NodeType)
            //    {
            //        case ExpressionType.Constant:
            //            var constantArg = (ConstantExpression)arg;
            //            paramameters[i] = constantArg.Value;
            //            break;
            //    }
            //}
            //return paramameters;
            return null;
        }

        /// <summary>
        /// Проверяет на эквивалентость параметров выражение в другой настроке.
        /// </summary>
        /// <param name="setupSetting">Экземпляр настроки Mocq.Setup.</param>
        /// <returns>true - если параметры совпадают, false в противном случае.</returns>
        public bool EqualsSetupParamaters(SetupSetting setupSetting)
        {
            return _setupParameters.SequenceEqual(setupSetting._setupParameters);
        }

        /// <summary>
        /// Проверяет, подходяд ли аргументы вызова функции к параметра настройки Mock.Setup.
        /// </summary>
        /// <param name="objects">Список аргументов вызываемой функции</param>
        /// <returns>true - если параметры совпадают, false в противном случае.</returns>
        public bool EqualsSetupParametersToObjectList(List<object> objects)
        {
            return _setupParameters.SequenceEqual(objects);
        }
    }
}
