using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BUnit.Moq.Setups
{
    public class SetupSettingFunction : SetupSettingAction
    {
        internal SetupSettingFunction(LambdaExpression lambdaExpression) : base(lambdaExpression)
        {
        }

        public TRetun Return<TRetun>()
        {
            return default(TRetun);
        }
    }
}
