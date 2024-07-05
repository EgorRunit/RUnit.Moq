using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BUnit.Moq.Setups
{
    /// <summary>
    /// Класс для обработки метода Mock.Setup().
    /// </summary>
    public class SetupSetting : IEquatable<SetupSetting>
    {
        readonly List<SetupArgument> _setupParameters;

        /// <summary>
        /// Количество в лямда выражения аргументов с любым значением.
        /// Чем их большеб тем меньшк ранг настройки выражения для Mock.Setup.
        /// </summary>
        readonly internal int AnyCount;
        /// <summary>
        /// Согнатура метода при динамическом вызове.
        /// </summary>
        readonly internal string MethodCallSignature;
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

            MethodCallSignature = methodCallExpression.ToString();
            MethodCallSignature = MethodCallSignature.Substring(MethodCallSignature.IndexOf('.') + 1);

            var method = methodCallExpression.Method;
            MethodOriginalSignature = method.ToString();

            _setupParameters = methodCallExpression.Arguments.ToSetupParameterList();
            AnyCount = _setupParameters.Where(x => x.SetupArgumentType == SetupArgumentType.AnyValue).Count();
        }

        public static bool operator == (SetupSetting mss1, SetupSetting mss2)
        {
            return Enumerable.SequenceEqual<SetupArgument>(mss1._setupParameters, mss2._setupParameters);
        }

        public static bool operator != (SetupSetting mss1, SetupSetting mss2)
        {
            return !Enumerable.SequenceEqual<SetupArgument>(mss1._setupParameters, mss2._setupParameters);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SetupSetting);
        }

        public override int GetHashCode()
        {
            return this.MethodCallSignature.GetHashCode();
        }

        public bool Equals(SetupSetting other)
        {
            return ReferenceEquals(other, this);
        }
    }
}