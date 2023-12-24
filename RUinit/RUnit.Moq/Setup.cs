using RUnIt.Moq.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RUnIt.Moq
{
    public class Setup : ISetup
    {
        /// <summary>
        /// Действие привязанное через метод Callback.
        /// </summary>
        Action _action;

        /// <summary>
        /// Название метода который переопределяют.
        /// </summary>
        public string MethodName { get; private set; }

        /// <summary>
        /// Сигнатура переопределенного метода.
        /// Список аргументов.
        /// </summary>
        public string MethodSignature { get; private set; }

        /// <summary>
        /// Полная сигнатура переопределенного метода.
        /// MethodName + MethodSignature
        /// </summary>
        public string FullMethosSignatire { get; private set; }

        /// <summary>
        /// Список аргументов переопределенного метода
        /// </summary>
        public ReadOnlyCollection<Expression> Arguments { get; private set; }

        internal Setup(MethodCallExpression methodCallExpression)
        {
            var methodInfo = methodCallExpression.Method;
            Arguments = methodCallExpression.Arguments;

            var methodSignature = new StringBuilder("(");
            foreach (var argument in Arguments)
            {

                methodSignature.Append(argument.ToString());
                methodSignature.Append(", ");
            }
            methodSignature.Replace(", ", ")", methodSignature.Length - 2, 2);

            MethodName = methodInfo.Name;
            MethodSignature = methodSignature.ToString();
            FullMethosSignatire = $"{MethodName} {MethodSignature}";
        }

        public void Callback(Action action)
        {
            _action = action;
        }

        //public TReturn Calbback<TReturn>(Func<TReturn> function)
        //{
        //    _function = function;
        //}
    }
}
