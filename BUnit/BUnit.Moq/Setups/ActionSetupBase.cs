using System;
using System.Linq.Expressions;
using System.Reflection;
using static System.Collections.Specialized.BitVector32;

namespace BUnit.Moq.Setups
{
    public class ActionSetupBase
    {
        /// <summary>
        /// Системная сигнатура метода
        /// </summary>
        public string MethodSignature { get; protected set; }
        /// <summary>
        /// Согнатура метода при динамическом вызове
        /// </summary>
        public string MethodCallSignature { get; protected set; }
        /// <summary>
        /// Сслыка на обрабную функция вызов
        /// </summary>
        protected Action callback;

        protected MethodCallExpression methodCallExpression;


        /// <summary>
        /// Конструктор
        /// </summary>
        public void ExecuteCallback()
        {
            callback();
        }

        protected void buildSetupArguments(MethodCallExpression methodCallExpression)
        {

        }

        protected object[] buildDynamicCallback()
        {
            var paramameters = new object[methodCallExpression.Arguments.Count];
            for (var i = 0; i < methodCallExpression.Arguments.Count; i++)
            {
                var arg = methodCallExpression.Arguments[i];
                switch (arg.NodeType)
                {
                    case ExpressionType.Constant:
                        var constantArg = (ConstantExpression)arg;
                        paramameters[i] = constantArg.Value;
                        break;
                }
            }
            return paramameters;
        }
    }
}
