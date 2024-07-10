using BUnit.Moq.Exceptions;
using System;
using System.Reflection;

namespace BUnit.Moq
{
    public partial class Assert
    {
        /// <summary>
        /// Verifies that an object is exactly the given type (and not a derived type).
        /// </summary>
        /// <typeparam name="T">The type the object should be</typeparam>
        /// <param name="object">The object to be evaluated</param>
        /// <returns>The object, casted to type T when successful</returns>
        /// <exception cref="IsTypeException">Thrown when the object is not the given type</exception>
        public static T IsType<T>(object obj)
        {
            IsType(typeof(T), obj);
            return (T)obj;
        }

        /// <summary>
        /// Verifies that an object is exactly the given type (and not a derived type).
        /// </summary>
        /// <param name="expectedType">The type the object should be</param>
        /// <param name="object">The object to be evaluated</param>
        /// <exception cref="IsTypeException">Thrown when the object is not the given type</exception>
        public static void IsType(Type expectedType, object obj)
        {
            if (obj == null)
                throw new IsTypeException(expectedType.FullName, null);

            var actualType = obj.GetType();
            if (expectedType != actualType)
            {
                var expectedTypeName = expectedType.FullName;
                var actualTypeName = actualType.FullName;

                if (expectedTypeName == actualTypeName)
                {
                    expectedTypeName += string.Format(" ({0})", expectedType.GetTypeInfo().Assembly.GetName().FullName);
                    actualTypeName += string.Format(" ({0})", actualType.GetTypeInfo().Assembly.GetName().FullName);
                }

                throw new IsTypeException(expectedTypeName, actualTypeName);
            }
        }
    }
}
