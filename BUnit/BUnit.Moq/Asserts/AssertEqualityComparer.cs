using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BUnit.Moq
{
    /// <summary>
    /// Default implementation of <see cref="IEqualityComparer{T}"/> used by the xUnit.net equality assertions.
    /// </summary>
    /// <typeparam name="T">The type that is being compared.</typeparam>
    class AssertEqualityComparer<T> : IEqualityComparer<T>
    {
        static readonly IEqualityComparer DefaultInnerComparer = new AssertEqualityComparerAdapter<object>(new AssertEqualityComparer<object>());
        static readonly TypeInfo NullableTypeInfo = typeof(Nullable<>).GetTypeInfo();

        readonly Func<IEqualityComparer> innerComparerFactory;

        public AssertEqualityComparer(IEqualityComparer innerComparer = null)
        {
            // Use a thunk to delay evaluation of DefaultInnerComparer
            innerComparerFactory = () => innerComparer ?? DefaultInnerComparer;
        }

        public bool Equals(T x, T y)
        {
            int? mismatchIndex;

            return Equals(x, y, out mismatchIndex);
        }

        /// <inheritdoc/>
        public bool Equals(T x, T y, out int? mismatchIndex)
        {
            mismatchIndex = null;
            var typeInfo = typeof(T).GetTypeInfo();

            // Null?
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            // Implements IEquatable<T>?
            var equatable = x as IEquatable<T>;
            if (equatable != null)
                return equatable.Equals(y);

            // Implements IComparable<T>?
            var comparableGeneric = x as IComparable<T>;
            if (comparableGeneric != null)
            {
                try
                {
                    return comparableGeneric.CompareTo(y) == 0;
                }
                catch
                {
                    // Some implementations of IComparable<T>.CompareTo throw exceptions in
                    // certain situations, such as if x can't compare against y.
                    // If this happens, just swallow up the exception and continue comparing.
                }
            }

            // Implements IComparable?
            var comparable = x as IComparable;
            if (comparable != null)
            {
                try
                {
                    return comparable.CompareTo(y) == 0;
                }
                catch
                {
                    // Some implementations of IComparable.CompareTo throw exceptions in
                    // certain situations, such as if x can't compare against y.
                    // If this happens, just swallow up the exception and continue comparing.
                }
            }

            // Dictionaries?
            var dictionariesEqual = CheckIfDictionariesAreEqual(x, y);
            if (dictionariesEqual.HasValue)
                return dictionariesEqual.GetValueOrDefault();

            // Sets?
            var setsEqual = CheckIfSetsAreEqual(x, y, typeInfo);
            if (setsEqual.HasValue)
                return setsEqual.GetValueOrDefault();

            // Enumerable?
            var enumerablesEqual = CheckIfEnumerablesAreEqual(x, y, out mismatchIndex);
            if (enumerablesEqual.HasValue)
            {
                if (!enumerablesEqual.GetValueOrDefault())
                {
                    return false;
                }

                // Array.GetEnumerator() flattens out the array, ignoring array ranks and lengths
                var xArray = x as Array;
                var yArray = y as Array;
                if (xArray != null && yArray != null)
                {
                    // new object[2,1] != new object[2]
                    if (xArray.Rank != yArray.Rank)
                        return false;

                    // new object[2,1] != new object[1,2]
                    for (var i = 0; i < xArray.Rank; i++)
                        if (xArray.GetLength(i) != yArray.GetLength(i))
                            return false;
                }

                return true;
            }

            // Implements IStructuralEquatable?
            var structuralEquatable = x as IStructuralEquatable;
            if (structuralEquatable != null && structuralEquatable.Equals(y, new TypeErasedEqualityComparer(innerComparerFactory())))
                return true;

            // Implements IEquatable<typeof(y)>?
            var iequatableY = typeof(IEquatable<>).MakeGenericType(y.GetType()).GetTypeInfo();
            if (iequatableY.IsAssignableFrom(x.GetType().GetTypeInfo()))
            {
                var equalsMethod = iequatableY.GetDeclaredMethod(nameof(IEquatable<T>.Equals));
                if (equalsMethod == null)
                    return false;

                return (bool)equalsMethod.Invoke(x, new object[] { y });
            }

            // Implements IComparable<typeof(y)>?
            var icomparableY = typeof(IComparable<>).MakeGenericType(y.GetType()).GetTypeInfo();
            if (icomparableY.IsAssignableFrom(x.GetType().GetTypeInfo()))
            {
                var compareToMethod = icomparableY.GetDeclaredMethod(nameof(IComparable<T>.CompareTo));
                if (compareToMethod == null)
                    return false;

                try
                {
                    return (int)compareToMethod.Invoke(x, new object[] { y }) == 0;
                }
                catch
                {
                    // Some implementations of IComparable.CompareTo throw exceptions in
                    // certain situations, such as if x can't compare against y.
                    // If this happens, just swallow up the exception and continue comparing.
                }
            }

            // Last case, rely on object.Equals
            return object.Equals(x, y);
        }

        bool? CheckIfEnumerablesAreEqual(T x, T y, out int? mismatchIndex)
        {
            mismatchIndex = null;

            var enumerableX = x as IEnumerable;
            var enumerableY = y as IEnumerable;

            if (enumerableX == null || enumerableY == null)
                return null;

            var enumeratorX = default(IEnumerator);
            var enumeratorY = default(IEnumerator);

            try
            {
                enumeratorX = enumerableX.GetEnumerator();
                enumeratorY = enumerableY.GetEnumerator();
                var equalityComparer = innerComparerFactory();

                mismatchIndex = 0;

                while (true)
                {
                    var hasNextX = enumeratorX.MoveNext();
                    var hasNextY = enumeratorY.MoveNext();

                    if (!hasNextX || !hasNextY)
                    {
                        if (hasNextX == hasNextY)
                        {
                            mismatchIndex = null;
                            return true;
                        }

                        return false;
                    }

                    if (!equalityComparer.Equals(enumeratorX.Current, enumeratorY.Current))
                        return false;

                    mismatchIndex++;
                }
            }
            finally
            {
                var asDisposable = enumeratorX as IDisposable;
                if (asDisposable != null)
                    asDisposable.Dispose();
                asDisposable = enumeratorY as IDisposable;
                if (asDisposable != null)
                    asDisposable.Dispose();
            }
        }

        bool? CheckIfDictionariesAreEqual(T x, T y)
        {
            var dictionaryX = x as IDictionary;
            var dictionaryY = y as IDictionary;

            if (dictionaryX == null || dictionaryY == null)
                return null;

            if (dictionaryX.Count != dictionaryY.Count)
                return false;

            var equalityComparer = innerComparerFactory();
            var dictionaryYKeys = new HashSet<object>(dictionaryY.Keys.Cast<object>());

            foreach (var key in dictionaryX.Keys.Cast<object>())
            {
                if (!dictionaryYKeys.Contains(key))
                    return false;

                var valueX = dictionaryX[key];
                var valueY = dictionaryY[key];

                if (!equalityComparer.Equals(valueX, valueY))
                    return false;

                dictionaryYKeys.Remove(key);
            }

            return dictionaryYKeys.Count == 0;
        }

        static MethodInfo s_compareTypedSetsMethod;
        bool? CheckIfSetsAreEqual(T x, T y, TypeInfo typeInfo)
        {
            if (!IsSet(typeInfo))
                return null;

            var enumX = x as IEnumerable;
            var enumY = y as IEnumerable;
            if (enumX == null || enumY == null)
                return null;

            Type elementType;
            if (typeof(T).GenericTypeArguments.Length != 1)
                elementType = typeof(object);
            else
                elementType = typeof(T).GenericTypeArguments[0];

            if (s_compareTypedSetsMethod == null)
            {
                s_compareTypedSetsMethod = GetType().GetTypeInfo().GetDeclaredMethod(nameof(CompareTypedSets));
                if (s_compareTypedSetsMethod == null)
                    return false;
            }

            var method = s_compareTypedSetsMethod.MakeGenericMethod(new Type[] { elementType });
            return (bool)method.Invoke(this, new object[] { enumX, enumY });
        }

        bool CompareTypedSets<R>(IEnumerable enumX, IEnumerable enumY)
        {
            var setX = new HashSet<R>(enumX.Cast<R>());
            var setY = new HashSet<R>(enumY.Cast<R>());
            return setX.SetEquals(setY);
        }

        bool IsSet(TypeInfo typeInfo)
        {
            return typeInfo.ImplementedInterfaces
                .Select(i => i.GetTypeInfo())
                .Where(ti => ti.IsGenericType)
                .Select(ti => ti.GetGenericTypeDefinition())
                .Contains(typeof(ISet<>).GetGenericTypeDefinition());
        }

        /// <inheritdoc/>
        public int GetHashCode(T obj)
        {
            throw new NotImplementedException();
        }

        private class TypeErasedEqualityComparer : IEqualityComparer
        {
            readonly IEqualityComparer innerComparer;

            public TypeErasedEqualityComparer(IEqualityComparer innerComparer)
            {
                this.innerComparer = innerComparer;
            }

            static MethodInfo s_equalsMethod;
            public new bool Equals(object x, object y)
            {
                if (x == null)
                    return y == null;
                if (y == null)
                    return false;

                // Delegate checking of whether two objects are equal to AssertEqualityComparer.
                // To get the best result out of AssertEqualityComparer, we attempt to specialize the
                // comparer for the objects that we are checking.
                // If the objects are the same, great! If not, assume they are objects.
                // This is more naive than the C# compiler which tries to see if they share any interfaces
                // etc. but that's likely overkill here as AssertEqualityComparer<object> is smart enough.
                Type objectType = x.GetType() == y.GetType() ? x.GetType() : typeof(object);

                // Lazily initialize and cache the EqualsGeneric<U> method.
                if (s_equalsMethod == null)
                {
                    s_equalsMethod = typeof(TypeErasedEqualityComparer).GetTypeInfo().GetDeclaredMethod(nameof(EqualsGeneric));
                    if (s_equalsMethod == null)
                        return false;
                }

                return (bool)s_equalsMethod.MakeGenericMethod(objectType).Invoke(this, new object[] { x, y });
            }

            bool EqualsGeneric<U>(U x, U y)
                => new AssertEqualityComparer<U>(innerComparer: innerComparer).Equals(x, y);

            public int GetHashCode(object obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
