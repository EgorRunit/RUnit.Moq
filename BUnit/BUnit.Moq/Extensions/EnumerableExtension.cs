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

        public static bool SequenceEqual(this List<SetupArgument> setupParameters, List<object> objects)
        {
            for(var i=0; i<setupParameters.Count;i++)
            {
                var setupParameter = setupParameters[i];
                var callArgument = objects[i];
                if(setupParameter.Type != callArgument.GetType())
                {
                    return false;
                }
                if(setupParameter.SetupArgumentType == SetupArgumentType.AnyValue)
                {
                    continue;
                }
                if(setupParameter.SetupArgumentType == SetupArgumentType.Constant)
                {
                    if(setupParameter.Type == typeof(string))
                    {
                        if(setupParameter.Value.ToString() != callArgument.ToString())
                        {
                            return false;
                        }
                    } else if(!((ValueType)setupParameter.Value).Equals(callArgument))
                    {
                        return false;
                    }
                }
                if (setupParameter.SetupArgumentType == SetupArgumentType.MemberAccess)
                {
                    if(setupParameter.Type.IsValueType)
                    {
                        if(!((ValueType)setupParameter.Value).Equals(callArgument))
                        {
                            return false;
                        }
                    } else if(!ReferenceEquals(setupParameter.Value, callArgument))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //public static bool Compare(this List<SetupArgument> setupArguments, IEnumerable<object> arguments)
        //{
        //    return true;
        //}

        //public static List<SetupArgument> ToSetupParameterList(this IEnumerable<object> array)
        //{
        //    var parameters = new List<SetupArgument>();
        //    foreach (var arrayItem in array)
        //    {
        //        //parameters.Add(new SetupArgument(arrayItem));
        //    }
        //    return parameters;
        //}
    }
}
