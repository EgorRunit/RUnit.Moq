using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BUnit.Moq.Setups
{
    public class MockSetupSettings
    {
        Dictionary<string, List<MockSetupSetting>> _mockSetupSettings;

        public MockSetupSettings()
        {
            _mockSetupSettings = new Dictionary<string, List<MockSetupSetting>>();
        }

        public MockSetupSetting RegisterSetup(LambdaExpression lambdaExpression, ActionSetupBase actionSetupBase)
        {
            var setupSetting = new MockSetupSetting(lambdaExpression);
            if(!_mockSetupSettings.ContainsKey(setupSetting.MethodOriginalSignature))
            {
                _mockSetupSettings.Add(setupSetting.MethodOriginalSignature, new List<MockSetupSetting>());
                _mockSetupSettings[setupSetting.MethodOriginalSignature].Add(setupSetting);
            }
            else
            {
                var list = _mockSetupSettings[setupSetting.MethodOriginalSignature];
                var oldSetupSetting = list.FirstOrDefault(x => x == setupSetting);
                if(oldSetupSetting != null)
                {
                    list.Remove(oldSetupSetting);
                }
                list.Add(setupSetting);
            }
            return setupSetting;
        }
    }
}
