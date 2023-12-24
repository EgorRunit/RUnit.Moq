using RUnIt.Moq.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RUnIt.Moq.Abstracts
{
    public abstract class Mock
    {

        List<ISetup> _setups;

        /// <summary>
        /// <para>
        /// Ключь - название метода<br/>
        /// Значение - хз
        /// </para>
        /// </summary>
        Dictionary<string, string> _callbackSetupes;

        /// <summary>
        /// Получает настроенные в этом Mock настройки ISetup в хронологическом порядке.
        /// Первая - самая старая 
        /// </summary>
        public IReadOnlyList<ISetup> Setups
        {
            get
            {
                //Проверить на создание новой коллекции
                return _setups.AsReadOnly();
            }
        }

        public Mock()
        {
            _callbackSetupes = new Dictionary<string, string>();
            _setups = new List<ISetup>();
        }

        protected void Callback(MethodCallExpression methodCallExpression)
        {
            var methodInfo = methodCallExpression.Method;
            var methodSignature = methodInfo.ToString();
            if(!_callbackSetupes.ContainsKey(methodSignature))
            {
                _callbackSetupes.Add(methodSignature, methodSignature);
            }

            var callbackSetup = _callbackSetupes[methodSignature];
            var setup = new Setup(methodCallExpression);

            _setups.Add(null);
        }

    }
}
