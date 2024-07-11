using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BUnit.Moq
{
    internal class AssertEqualityComparerAdapter<T> : IEqualityComparer
    {
        private readonly IEqualityComparer<T> innerComparer;

        public AssertEqualityComparerAdapter(IEqualityComparer<T> innerComparer)
        {
            if (innerComparer == null)
            {
                throw new ArgumentNullException("innerComparer");
            }
            this.innerComparer = innerComparer;
        }

        public new bool Equals(object x, object y)
        {
            return innerComparer.Equals((T)x, (T)y);
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
