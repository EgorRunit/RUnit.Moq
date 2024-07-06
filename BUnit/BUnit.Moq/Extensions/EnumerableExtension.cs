using BUnit.Moq.Setups;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
    public static class EnumerableExtension
    {
        public static List<SetupArgument> ToSetupParameterList(this IEnumerable<Expression> array)
        {
            var parameters = new List<SetupArgument>();
            foreach (var arrayItem in array)
            {
                parameters.Add(new SetupArgument(arrayItem));
            }
            return parameters;
        }

        public static List<SetupArgument> ToSetupParameterList(this IEnumerable<object> array)
        {
            var parameters = new List<SetupArgument>();
            foreach (var arrayItem in array)
            {
                //parameters.Add(new SetupArgument(arrayItem));
            }
            return parameters;
        }
    }
}
