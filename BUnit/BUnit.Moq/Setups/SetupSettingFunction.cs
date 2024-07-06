using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BUnit.Moq.Setups
{
    internal class SetupSettingFunction : SetupSetting
    {
        public SetupSettingFunction(LambdaExpression lambdaExpression) : base(lambdaExpression)
        {
        }
    }
}
