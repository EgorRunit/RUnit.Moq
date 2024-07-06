using BUnit.Moq;
using BUnit.Moq.Setups;
using System.Linq.Expressions;
using System.Text;

namespace BUnit.Test.Setups
{
    public class SetupSettingActionTest
    {
        void Write<T1, T2, T3>(T1 t1, T2 t2, T3 t3) { }

        [Fact]
        public void IntConst_StringConst_ByteConst()
        {
            //arrange
            Expression<Action> expressionAction = () => Write(5, "4444", (byte)3);
            var setupSetting = new SetupSettingAction(expressionAction);

            //assert
            Assert.True(setupSetting.AnyCount == 0);
            Assert.True(setupSetting.MethodOriginalSignature == "Void Write[Int32,String,Byte](Int32, System.String, Byte)");
        }

        [Fact]
        public void IntConst_StringConst_AnyValue()
        {
            //arrange
            Expression<Action> expressionAction = () => Write(5, "4444", new StringBuilder());
            var setupSetting = new SetupSettingAction(expressionAction);

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
            var setupSetting = new SetupSettingAction(expressionAction);

            //assert
            Assert.True(setupSetting.AnyCount == 0);
            Assert.True(setupSetting.MethodOriginalSignature == "Void Write[Int32,String,StringBuilder](Int32, System.String, System.Text.StringBuilder)");
        }

        [Fact]
        public void Callback_IntConst_StringConst_MemberAccess()
        {
            //arrange
            var sb = new StringBuilder();
            Expression<Action> expressionAction = () => Write(5, "4444", sb);
            var setupSetting = new SetupSettingAction(expressionAction);
            setupSetting.Callback<int,string, StringBuilder>((i, s, sb1) =>{
                sb1.Append("string builder");
            });

            //act
            setupSetting.Execute(new List<object>() { 5, "444", sb });

            //assert
            Assert.True(sb.ToString() == "string builder");
        }

        [Fact]
        public void Callback_IntMemberAccess_StringMember_MemberAccess()
        {
            //arrange
            var intValue = 5;
            var stringValue = "string member";
            var callbackIntValue = 0;
            var callbackStringValue = "";
            var sb = new StringBuilder();
            Expression<Action> expressionAction = () => Write(intValue, stringValue, sb);
            var setupSetting = new SetupSettingAction(expressionAction);
            setupSetting.Callback<int, string, StringBuilder>((i1, s, sb1) => {
                callbackIntValue = i1;
                callbackStringValue = s;
                sb1.Append("string builder");
            });

            //act
            setupSetting.Execute(new List<object>() { intValue, stringValue, sb });

            //assert
            Assert.True(stringValue == callbackStringValue);
            Assert.True(intValue == callbackIntValue);
            Assert.True(sb.ToString() == "string builder");
        }
    }
}
