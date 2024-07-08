using BUnit.Interfaces;
using BUnit.Moq.Structs;
using BUnit.Setups;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BUnit.Moq.Setups
{
    public class SetupSettingAction<T> : SetupSetting, ICallbackResult, ISetup<T> where T : class
    {
        /// <summary>
        /// Сслыка на обрабную функция вызов
        /// </summary>
        protected Delegate callback;

        public SetupSettingAction(LambdaExpression lambdaExpression) : base(lambdaExpression) { }

        //public void Callback(Action action)
        //{
        //    callback = action;
        //}

        //public void Callback<T1>(Action<T1> action)
        //{
        //    callback = action;
        //}

        public void Callback<T1, T2>(Action<T1, T2> action)
        {
            callback = action;
        }

        public void Callback<T1, T2, T3>(Action<T1, T2, T3> action)
        {
            callback = action;
        }

        public ICallbackResult Callback(InvocationAction action)
        {
            throw new NotImplementedException();
        }

        public ICallbackResult Callback(Delegate callback)
        {
            throw new NotImplementedException();
        }

        public ICallBaseResult CallBase()
        {
            throw new NotImplementedException();
        }

        public void ExecuteCallback(List<object> methodParameters)
        {
            callback.DynamicInvoke(methodParameters.ToArray());
        }

        public IVerifies Raises(Action<T> eventExpression, EventArgs args)
        {
            throw new NotImplementedException();
        }

        public IVerifies Raises(Action<T> eventExpression, Func<EventArgs> func)
        {
            throw new NotImplementedException();
        }

        public IVerifies Raises(Action<T> eventExpression, params object[] args)
        {
            throw new NotImplementedException();
        }

        public IThrowsResult Throws(Exception exception)
        {
            throw new NotImplementedException();
        }

        public IThrowsResult Throws<TException>() where TException : Exception, new()
        {
            throw new NotImplementedException();
        }

        public IThrowsResult Throws(Delegate exceptionFunction)
        {
            throw new NotImplementedException();
        }

        public IThrowsResult Throws<TException>(Func<TException> exceptionFunction) where TException : Exception
        {
            throw new NotImplementedException();
        }

        public IThrowsResult Throws<T, TException>(Func<T, TException> exceptionFunction) where TException : Exception
        {
            throw new NotImplementedException();
        }

        public void Verifiable()
        {
            throw new NotImplementedException();
        }

        public void Verifiable(string failMessage)
        {
            throw new NotImplementedException();
        }

        public void Verifiable(Times times)
        {
            throw new NotImplementedException();
        }

        public void Verifiable(Func<Times> times)
        {
            throw new NotImplementedException();
        }

        public void Verifiable(Times times, string failMessage)
        {
            throw new NotImplementedException();
        }

        public void Verifiable(Func<Times> times, string failMessage)
        {
            throw new NotImplementedException();
        }

        ICallbackResult ICallback.Callback(Action action)
        {
            throw new NotImplementedException();
        }

        ICallbackResult ICallback.Callback<T1>(Action<T1> action)
        {
            throw new NotImplementedException();
        }
    }
}
