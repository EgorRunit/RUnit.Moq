using BUnit.Moq.Verifies;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;

namespace BUnit.Moq
{
    public partial class Mock<T>
    {
        public void Verify(Expression<Action<T>> expression)
        {
            Verify(this, expression, Times.AtLeastOnce(), null);
        }

        public void Verify(Expression<Action<T>> expression, Times times)
        {
            Verify(this, expression, times, null);
        }

        public void Verify(Expression<Action<T>> expression, Func<Times> times)
        {
            Verify(this, expression, times(), null);
        }

        public void Verify(Expression<Action<T>> expression, string failMessage)
        {
            Verify(this, expression, Times.AtLeastOnce(), failMessage);
        }


        public void Verify(Expression<Action<T>> expression, Times times, string failMessage)
        {
            Verify(this, expression, times, failMessage);
        }


        public void Verify(Expression<Action<T>> expression, Func<Times> times, string failMessage)
        {
            Verify(this, expression, times(), failMessage);

        }







        internal static void Verify(Mock<T> mock, LambdaExpression expression, Times times, string failMessage)
        {

        }
    }
}
