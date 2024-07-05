using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BUnit.Moq.Setups
{
    public class CallbackManager
    {
        readonly SetupSettings _mockSetupSettings;
        internal CallbackManager(SetupSettings mockSetupSettings)
        {
            _mockSetupSettings = mockSetupSettings;
        }

        public void CallbackIfExists(List<ParameterInfo> parameterInfos)
        {

        }

        public void CallbackIfExists(List<object> parameters)
        {

        }
    }
}
