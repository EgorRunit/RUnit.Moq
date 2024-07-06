using BUnit.Moq.Setups;
using System.Linq.Expressions;
using System.Text;

namespace BUnit.Test.Setups
{
    public class SetupSettingsTest
    {
        SetupSettings _setupSettings;
        void Write<T1, T2, T3>(T1 t1, T2 t2, T3 t3) { }

        public SetupSettingsTest()
        {
            _setupSettings = new SetupSettings();
        }

        [Fact]
        public void IntConst_StringConst_ByteConst()
        {
            //arrange
            Expression<Action> expressionAction = () => Write(5, "4444", (byte)3);

            //act
            var setupSetting = _setupSettings.RegisterSetup(expressionAction, null);

            //assert
            Assert.True(setupSetting.AnyCount == 0);
            Assert.True(setupSetting.MethodOriginalSignature == "Void Write[Int32,String,Byte](Int32, System.String, Byte)");
        }

        [Fact]
        public void IntConst_StringConst_AnyValue()
        {
            //arrange
            Expression<Action> expressionAction = () => Write(5, "4444", new StringBuilder());

            //act
            var setupSetting = _setupSettings.RegisterSetup(expressionAction, null);

            //assert
            Assert.True(setupSetting.AnyCount == 1);
            Assert.True(setupSetting.MethodOriginalSignature == "Void Write[Int32,String,StringBuilder](Int32, System.String, System.Text.StringBuilder)");
        }

        [Fact]
        public void IntConst_StringConst_MemberAccess()
        {
            //arrange
            var sb = new StringBuilder();
            Expression<Action> expressionAction = () => Write(5, "4444", sb);

            //act
            var setupSetting = _setupSettings.RegisterSetup(expressionAction, null);

            //assert
            Assert.True(setupSetting.AnyCount == 0);
            Assert.True(setupSetting.MethodOriginalSignature == "Void Write[Int32,String,StringBuilder](Int32, System.String, System.Text.StringBuilder)");
        }

        [Fact]
        public void Refernce_Not_Equals()
        {
            //arrange
            Expression<Action> expressionAction = () => Write(5, "4444", (byte)3);

            //act
            var setupSetting1 = _setupSettings.RegisterSetup(expressionAction, null);
            var setupSetting2 = _setupSettings.RegisterSetup(expressionAction, null);

            //assert
            Assert.False(ReferenceEquals(setupSetting1, setupSetting2));
        }

    }
}
