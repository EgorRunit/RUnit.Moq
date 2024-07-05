using BUnit.Moq.Setups;
using System.Linq.Expressions;
using System.Text;

namespace BUnit.Test.Setups
{
    public class SetupSettingTest
    {
        void Write<T1, T2, T3>(T1 t1, T2 t2, T3 t3) { }

        [Fact]
        public void IntConst_StringConst_ByteConst()
        {
            //arrange
            Expression<Action> expressionAction = () => Write(5, "4444", (byte)3);
            var setupSetting = new SetupSetting(expressionAction);

            Assert.True(setupSetting.MethodCallSignature == "Test.Setups.SetupSettingTest).Write(5, \"4444\", 3)");
            Assert.True(setupSetting.MethodOriginalSignature == "Void Write[Int32,String,Byte](Int32, System.String, Byte)");
        }

        [Fact]
        public void IntConst_StringConst_AnyValue()
        {
            //arrange
            Expression<Action> expressionAction = () => Write(5, "4444", new StringBuilder());
            var setupSetting = new SetupSetting(expressionAction);

            Assert.True(setupSetting.MethodCallSignature == "Test.Setups.SetupSettingTest).Write(5, \"4444\", new StringBuilder())");
            Assert.True(setupSetting.MethodOriginalSignature == "Void Write[Int32,String,StringBuilder](Int32, System.String, System.Text.StringBuilder)");
        }

        [Fact]
        public void IntConst_StringConst_MemberAccess()
        {
            //arrange
            var sb = new StringBuilder();
            Expression<Action> expressionAction = () => Write(5, "4444", sb);
            var setupSetting = new SetupSetting(expressionAction);

            Assert.True(setupSetting.MethodOriginalSignature == "Void Write[Int32,String,StringBuilder](Int32, System.String, System.Text.StringBuilder)");
        }
    }
}
