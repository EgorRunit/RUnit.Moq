using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BUnit.Moq.Setups
{
    /// <summary>
    /// Класс для обработки метода Mock.Setup().
    /// </summary>
    public class MockSetupSetting : IEquatable<MockSetupSetting>
    {
        /// <summary>
        /// Экземпляр лямда выражения для Mock.Setup.
        /// </summary>
        LambdaExpression _lambdaExpression;
        Expression[] _lambdaExpressionArguments;

        /// <summary>
        /// Количество в лямда выражения аргументов с любым значением.
        /// Чем их большеб тем меньшк ранг настройки выражения для Mock.Setup.
        /// </summary>
        internal int AnyCount { get; private set; }

        /// <summary>
        /// Согнатура метода при динамическом вызове.
        /// </summary>
        public string MethodCallSignature { get; internal set; }
        /// <summary>
        /// Системная сигнатура метода
        /// </summary>
        public string MethodOriginalSignature { get; internal set; }

        public DateTime Date
        {
            get; internal set;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="lambdaExpression">Экземпляр лямда выражения для Mock.Setup.</param>
        public MockSetupSetting(LambdaExpression lambdaExpression)
        {
            Date = DateTime.Now;
            _lambdaExpression = lambdaExpression;
            var methodCallExpression = lambdaExpression.Body as MethodCallExpression;

            MethodCallSignature = methodCallExpression.ToString();
            MethodCallSignature = MethodCallSignature.Substring(MethodCallSignature.IndexOf('.') + 1);

            var method = methodCallExpression.Method;
            MethodOriginalSignature = method.ToString();

            _lambdaExpressionArguments = methodCallExpression.Arguments.ToArray();
            var ddd = methodCallExpression.Arguments.ToSetupParameterList();
            AnyCount = _lambdaExpressionArguments.Where(x => x.NodeType == ExpressionType.Call && x.ToString() == "Any()").Count();
        }

        public static bool operator ==(MockSetupSetting mss1, MockSetupSetting mss2)
        {
            return _compareListExpression(mss1, mss2);
        }

        public static bool operator !=(MockSetupSetting mss1, MockSetupSetting mss2)
        {
            return !_compareListExpression(mss1, mss2);
        }

        static bool _compareListExpression(MockSetupSetting mss1, MockSetupSetting mss2)
        {
            if (ReferenceEquals(mss1, null))
            {
                return false;
            }
            if (ReferenceEquals(mss2, null))
            {
                return false;
            }
            if (mss1.AnyCount != mss2.AnyCount)
            {
                return false;
            }
            if (mss1._lambdaExpressionArguments.Length != mss2._lambdaExpressionArguments.Length)
            {
                return false;
            }
            var lea1 = mss1._lambdaExpressionArguments;
            var lea2 = mss1._lambdaExpressionArguments;
            for (var i = 0; i < lea1.Length; i++)
            {
                var exp1 = lea1[i];
                var exp2 = lea2[i];
                if (exp1.NodeType != exp2.NodeType || exp1.Type != exp2.Type)
                {
                    return false;
                }
                switch (exp1.NodeType)
                {
                    case ExpressionType.Constant:
                        var constant1 = (ConstantExpression)exp1;
                        var constant2 = (ConstantExpression)exp2;
                        return constant1.Value == constant2.Value;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MockSetupSetting);
        }

        public override int GetHashCode()
        {
            return this.MethodCallSignature.GetHashCode();
        }

        public bool Equals(MockSetupSetting other)
        {
            return ReferenceEquals(other, this);
        }
    }
}
