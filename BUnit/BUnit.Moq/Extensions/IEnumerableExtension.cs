using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace System.Linq
{
    public static class IEnumerableExtension
    {
        public static List<SetupArgument> ToSetupParameterList(this IEnumerable<Expression> array)
        {
            var parameters = new List<SetupArgument>();
            foreach(var arrayItem in array) 
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
                        var fieldExpression = arrayItem as MemberExpression;
                        constantExpression = fieldExpression.Expression as ConstantExpression;
                        parameter.SetupArgumentType = SetupArgumentType.MemberAccess;
                        parameter.Value = constantExpression.Value;
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
