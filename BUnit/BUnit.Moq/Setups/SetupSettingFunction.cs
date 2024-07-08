using BUnit.Interfaces;
using BUnit.Moq.Structs;
using BUnit.Setups;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BUnit.Moq.Setups
{
    public class SetupSettingFunction<TMock, TResult> :  SetupSetting , ISetup<TMock, TResult>, IReturnsThrows<TMock, TResult> where TMock : class
    {
        /// <summary>
        /// Сслыка на обрабную функция вызов
        /// </summary>
        protected Delegate callback;
        public SetupSettingFunction(LambdaExpression lambdaExpression) : base(lambdaExpression) { }
        protected Func<object> returns;



        public object Return()
        {
            if (returns != null)
            {
                return returns();
            }
            else
            {
                return default;
            }
        }










        public IReturnsResult<TMock> Returns(TResult value)
        {
            throw new NotImplementedException();
        }

        public IReturnsResult<TMock> Returns(InvocationFunc valueFunction)
        {
            throw new NotImplementedException();
        }

        public IReturnsResult<TMock> Returns(Delegate valueFunction)
        {
            throw new NotImplementedException();
        }

        public IReturnsResult<TMock> Returns(Func<TResult> valueFunction)
        {
            throw new NotImplementedException();
        }

        public IReturnsResult<TMock> Returns<T>(Func<T, TResult> valueFunction)
        {
            throw new NotImplementedException();
        }

        public IReturnsResult<TMock> CallBase()
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

        public IReturnsThrows<TMock, TResult> Callback(InvocationAction action)
        {
            throw new NotImplementedException();
        }

        public IReturnsThrows<TMock, TResult> Callback(Delegate callback)
        {
            throw new NotImplementedException();
        }

        public IReturnsThrows<TMock, TResult> Callback(Action action)
        {
            throw new NotImplementedException();
        }

        public IReturnsThrows<TMock, TResult> Callback<T>(Action<T> action)
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
    }
}
