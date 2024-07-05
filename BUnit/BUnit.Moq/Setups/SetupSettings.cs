using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BUnit.Moq.Setups
{
    internal class SetupSettings
    {
        Dictionary<string, List<SetupSetting>> _mockSetupSettings;

        internal SetupSettings()
        {
            _mockSetupSettings = new Dictionary<string, List<SetupSetting>>();
        }

        internal SetupSetting RegisterSetup(LambdaExpression lambdaExpression, ActionSetupBase actionSetupBase)
        {
            var setupSetting = new SetupSetting(lambdaExpression);
            if(!_mockSetupSettings.ContainsKey(setupSetting.MethodOriginalSignature))
            {
                _mockSetupSettings.Add(setupSetting.MethodOriginalSignature, new List<SetupSetting>());
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

        internal SetupSetting TryGetSetupSetting(SetupSetting setupSetting)
        {
            return null;
                
        }
    }
}
