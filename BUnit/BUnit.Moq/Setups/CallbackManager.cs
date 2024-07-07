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

        public SetupSetting TryGetSetupSetting(string methodOriginalSignature, List<object> methodArguments)
        {
            var mockSettingSetting = _mockSetupSettings.TryGetSetupSetting(methodOriginalSignature, methodArguments);
            return mockSettingSetting;
        }

    }
}
