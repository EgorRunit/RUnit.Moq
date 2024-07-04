using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace System.Collections.Generic
{
    public enum SetupArgumentType
    {
        Constant,
        Ref,
        MemberAccess
    }

    public class SetupArgument
    {
        object _value;
        public MemberExpression MemberExpression { get; set; }

        public SetupArgumentType SetupArgumentType { get; set; }

        public object Value
        {
            get
            {
                if (MemberExpression != null)
                {
                    if (Type.IsValueType)
                    {
                        object container = ((ConstantExpression)MemberExpression.Expression).Value;
                        object value = ((FieldInfo)MemberExpression.Member).GetValue(container);
                        return value;
                    }
                    else
                    {
                        object container = ((ConstantExpression)MemberExpression.Expression).Value;
                        object value = ((FieldInfo)MemberExpression.Member).GetValue(container);
                        return value;
                    }
                }
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public Type Type { get; set; }
    }

    public static class ArrayExtension
    {
        public static SetupArgument[] ConvertToParameters(this Expression[] array)
        {
            var parameters = new SetupArgument[array.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                var arrayItem = array[i];
                parameters[i] = new SetupArgument()
                {
                    Type = arrayItem.Type,
                };
                ConstantExpression constantExpression;
                switch(arrayItem.NodeType)
                {
                    case ExpressionType.Constant:
                        constantExpression = arrayItem as ConstantExpression;
                        parameters[i].SetupArgumentType = SetupArgumentType.Constant;
                        parameters[i].Value = constantExpression.Value;
                        break;
                    case ExpressionType.MemberAccess:
                        var fieldExpression = arrayItem as MemberExpression;
                        constantExpression = fieldExpression.Expression as ConstantExpression;
                        parameters[i].SetupArgumentType = SetupArgumentType.MemberAccess;
                        parameters[i].Value = constantExpression.Value;
                        break;
                    default:
                        parameters[i].SetupArgumentType = SetupArgumentType.Ref;
                        break;
                }
            }
            return parameters;
        }

        public static void ConvertToSetupParameters(this ParameterInfo[] parameterInfos)
        {
            var setupArgument = new SetupArgument[parameterInfos.Length];
            for(var i=0; i < parameterInfos.Length; i++)
            {
                var parameterInfo = parameterInfos[i];
                //switch(parameterInfo.)
            }
            //return null;
        }

    }
}
