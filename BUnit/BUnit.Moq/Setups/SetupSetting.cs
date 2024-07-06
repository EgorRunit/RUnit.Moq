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

        public static bool operator ==(SetupSetting mss1, SetupSetting mss2)
        {
            if (ReferenceEquals(mss1, null))
            {
                return false;
            }
            if (ReferenceEquals(mss2, null))
            {
                return false;
            }
            return Enumerable.SequenceEqual<SetupArgument>(mss1._setupParameters, mss2._setupParameters);
        }

        public static bool operator !=(SetupSetting mss1, SetupSetting mss2)
        {
            if (ReferenceEquals(mss1, null))
            {
                return true;
            }
            if (ReferenceEquals(mss2, null))
            {
                return true;
            }
            return !Enumerable.SequenceEqual<SetupArgument>(mss1._setupParameters, mss2._setupParameters);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SetupSetting);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        public bool Equals(SetupSetting other)
        {
            return ReferenceEquals(other, this);
        }
    }
}
