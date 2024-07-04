using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BUnit.Moq.Setups
{
    /// <summary>
    /// Класс описывает один аргумент в expression для метода Mock.Setup().
    /// </summary>
    public class SetupArgument : IEquatable<SetupArgument>
    {
        /// <summary>
        /// Экземпляр функции возвращающей текущее значение аргумента в expression.
        /// </summary>
        readonly Func<object> _value;
        /// <summary>
        /// Тип аргумента в expression.
        /// </summary>
        readonly public SetupArgumentType SetupArgumentType;
        /// <summary>
        /// Системный тип аргумента.
        /// </summary>
        readonly public Type Type;
        /// <summary>
        /// Возвращает текущее значение аргумента в expression.
        /// </summary>
        public object Value => _value();

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="expression">Вырежение описываещее аргумент в Mock.Setup().</param>
        public SetupArgument(Expression expression)
        {
            ConstantExpression constantExpression;
            Type = expression.Type;
            switch (expression.NodeType)
            {
                case ExpressionType.Constant:
                    constantExpression = expression as ConstantExpression;
                    SetupArgumentType = SetupArgumentType.Constant;
                    _value = () =>  constantExpression.Value;
                    break;
                case ExpressionType.MemberAccess:
                    var memberExpression = expression as MemberExpression;
                    constantExpression = memberExpression.Expression as ConstantExpression;
                    SetupArgumentType = SetupArgumentType.MemberAccess;
                    object container = constantExpression.Value;
                    _value = () => ((FieldInfo)memberExpression.Member).GetValue(container);
                    break;
                default:
                    SetupArgumentType = SetupArgumentType.AnyValue;
                    break;
            }
        }

        public bool Equals(SetupArgument other)
        {
            return Type == other.Type && SetupArgumentType == other.SetupArgumentType;
        }
    }
}
