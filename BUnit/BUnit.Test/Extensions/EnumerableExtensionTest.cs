using BUnit.Moq.Setups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BUnit.Test.Extensions
{
    public class EnumerableExtensionTest
    {
        void Write<T1, T2, T3>(T1 t1, T2 t2, T3 t3) { }


        [Fact]
        public void IntConstant_StringConstant_ByteConstant()
        {
            //arrange
            Expression<Action> expressionAction = () => Write(5, "4444", (byte)3);

            //act
            var expresions = expressionAction.GetExpressions();

            //assert
            Assert.True(expresions.Count == 3);
            Assert.True(expresions[0].SetupArgumentType == SetupArgumentType.Constant);
            Assert.True(expresions[1].SetupArgumentType == SetupArgumentType.Constant);
            Assert.True(expresions[2].SetupArgumentType == SetupArgumentType.Constant);
            Assert.True(expresions[0].Type == typeof(int));
            Assert.True(expresions[1].Type == typeof( string));
            Assert.True(expresions[2].Type == typeof(byte));
            Assert.True(Convert.ToInt32(expresions[0].Value) == 5);
            Assert.True(Convert.ToString(expresions[1].Value) == "4444");
            Assert.True(Convert.ToByte(expresions[2].Value) == 3);
        }


        [Fact]
        public void IntConstan_StringBuilderAnyValue_ByteConstant()
        {
            //arrange
            Expression<Action> expressionAction = () => Write(5, new StringBuilder(), (byte)3);

            //act
            var expresions = expressionAction.GetExpressions();

            //assert
            Assert.True(expresions.Count == 3);
            Assert.True(expresions[0].SetupArgumentType == SetupArgumentType.Constant);
            Assert.True(expresions[1].SetupArgumentType == SetupArgumentType.AnyValue);
            Assert.True(expresions[2].SetupArgumentType == SetupArgumentType.Constant);
            Assert.True(expresions[0].Type == typeof(int));
            Assert.True(expresions[1].Type == typeof(StringBuilder));
            Assert.True(expresions[2].Type == typeof(byte));
            Assert.True(Convert.ToInt32(expresions[0].Value) == 5);
            Assert.True(Convert.ToByte(expresions[2].Value) == 3);
        }

        [Fact]
        public void IntMember_StringBuilderAnyValue_ByteConstant()
        {
            //arrange
            var intValue = 10;
            Expression<Action> expressionAction = () => Write(intValue, new StringBuilder(), (byte)3);

            //act
            var expresions = expressionAction.GetExpressions();

            //assert
            Assert.True(expresions.Count == 3);
            Assert.True(expresions[0].SetupArgumentType == SetupArgumentType.MemberAccess);
            Assert.True(expresions[1].SetupArgumentType == SetupArgumentType.AnyValue);
            Assert.True(expresions[2].SetupArgumentType == SetupArgumentType.Constant);
            Assert.True(expresions[0].Type == typeof(int));
            Assert.True(expresions[1].Type == typeof(StringBuilder));
            Assert.True(expresions[2].Type == typeof(byte));
            Assert.True(Convert.ToInt32(expresions[0].Value) == intValue);
            Assert.True(Convert.ToByte(expresions[2].Value) == 3);
        }

        [Fact]
        public void IntMember_StringBuilderMember_ByteConstant()
        {
            //arrange
            var intValue = 10;
            var stringBuilderValue = new StringBuilder();
            Expression<Action> expressionAction = () => Write(intValue, stringBuilderValue, (byte)3);

            //act
            var expresions = expressionAction.GetExpressions();

            //assert
            Assert.True(expresions.Count == 3);
            Assert.True(expresions[0].SetupArgumentType == SetupArgumentType.MemberAccess);
            Assert.True(expresions[1].SetupArgumentType == SetupArgumentType.MemberAccess);
            Assert.True(expresions[2].SetupArgumentType == SetupArgumentType.Constant);
            Assert.True(expresions[0].Type == typeof(int));
            Assert.True(expresions[1].Type == typeof(StringBuilder));
            Assert.True(expresions[2].Type == typeof(byte));
            Assert.True(Convert.ToInt32(expresions[0].Value) == intValue);
            Assert.True(ReferenceEquals(expresions[1].Value, stringBuilderValue));
            Assert.True(Convert.ToByte(expresions[2].Value) == 3);
        }
    }
}
