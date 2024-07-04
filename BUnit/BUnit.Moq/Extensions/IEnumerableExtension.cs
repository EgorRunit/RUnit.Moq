using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
    public static class IEnumerableExtension
    {
        public static List<SetupArgument> ToSetupParameterList(this IEnumerable<Expression> array)
        {
            var parameters = new List<SetupArgument>();
            foreach (var arrayItem in array)
            {
                var parameter = new SetupArgument()
                {
                    Type = arrayItem.Type,
                };
                parameters.Add(parameter);
                ConstantExpression constantExpression;
                switch (arrayItem.NodeType)
                {
                    case ExpressionType.Constant:
                        constantExpression = arrayItem as ConstantExpression;
                        parameter.SetupArgumentType = SetupArgumentType.Constant;
                        parameter.Value = constantExpression.Value;
                        break;
                    case ExpressionType.MemberAccess:
                        var memberExpression = arrayItem as MemberExpression;
                        constantExpression = memberExpression.Expression as ConstantExpression;
                        parameter.SetupArgumentType = SetupArgumentType.MemberAccess;
                        if (parameter.Type.IsValueType)
                        {
                            parameter.MemberExpression= memberExpression;
                        }
                        else
                        {
                            parameter.MemberExpression = memberExpression;
                            //parameter.Value = constantExpression.Value;
                        }
                        break;
                    default:
                        parameter.SetupArgumentType = SetupArgumentType.Ref;
                        break;
                }
            }
            return parameters;
        }
    }
}
