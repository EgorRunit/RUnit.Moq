using BUnit.Moq.Exceptions;
using System.Reflection;

namespace BUnit.Moq
{
    public partial class Assert
    {
        public static void IsAssignableFrom<T>(object obj)
        {
            var expectedType = typeof(T);
            if (obj == null || !expectedType.GetTypeInfo().IsAssignableFrom(obj.GetType().GetTypeInfo()))
            {
                throw new IsAssignableFromException(expectedType, obj);
            }
        }
    }
}
