using BUnit.Moq.Setups;
using System.Linq.Expressions;
using System.Text;

namespace BUnit.Test.Setups
{
    public class SetupArgumentTest
    {
        void Write<T>(T a) { }

        [Fact]
        public void Expression_With_ValueType_Constant()
        {
            //arrange
            Expression<Action> expressionAction = () => Write(5);
            var setupArgument = new SetupArgument(expressionAction.GetExpression(0));

            //assert
            Assert.True(setupArgument.Type == typeof(int));
            Assert.True(setupArgument.SetupArgumentType == SetupArgumentType.Constant);
            Assert.True(Convert.ToInt32(setupArgument.Value) == 5);
        }

        [Fact]
        public void Expression_With_ValueType_Member()
        {
            //arrange
            var cn = 5;
            Expression<Action> expressionAction = () => Write(cn);
            var setupArgument = new SetupArgument(expressionAction.GetExpression(0));

            //act
            cn = 6;

            //assert
            Assert.True(setupArgument.Type == typeof(int));
            Assert.True(setupArgument.SetupArgumentType == SetupArgumentType.MemberAccess);
            Assert.True(Convert.ToInt32(setupArgument.Value) == cn);
        }

        [Fact]
        public void Expression_With_ReferenceType_New()
        {
            //arrange
            Expression<Action> expressionAction = () => Write(new StringBuilder());
            var setupArgument = new SetupArgument(expressionAction.GetExpression(0));

            //assert
            Assert.True(setupArgument.Type == typeof(StringBuilder));
            Assert.True(setupArgument.SetupArgumentType == SetupArgumentType.AnyValue);
        }

        [Fact]
        public void Expression_With_ReferenceType_Member()
        {
            //arrange
            var sb = new StringBuilder();
            Expression<Action> expressionAction = () => Write(sb);
            var setupArgument = new SetupArgument(expressionAction.GetExpression(0));

            //assert
            Assert.True(setupArgument.Type == typeof(StringBuilder));
            Assert.True(setupArgument.SetupArgumentType == SetupArgumentType.MemberAccess);
            Assert.True(sb.Equals(setupArgument.Value));
        }

        [Fact]
        public void Expression_With_Enum_Constant()
        {
            //arrange
            Expression<Action> expressionAction = () => Write(DayOfWeek.Friday);
            var setupArgument = new SetupArgument(expressionAction.GetExpression(0));

            //assert
            Assert.True(setupArgument.Type == typeof(DayOfWeek));
            Assert.True(setupArgument.SetupArgumentType == SetupArgumentType.Constant);
            Assert.True((DayOfWeek)setupArgument.Value == DayOfWeek.Friday);
        }

        [Fact]
        public void Expression_With_Enum_Member()
        {
            //arrange
            var enumValue = DayOfWeek.Friday;
            Expression<Action> expressionAction = () => Write(enumValue);
            var setupArgument = new SetupArgument(expressionAction.GetExpression(0));

            //assert
            Assert.True(setupArgument.Type == typeof(DayOfWeek));
            Assert.True(setupArgument.SetupArgumentType == SetupArgumentType.MemberAccess);
            Assert.True((DayOfWeek)setupArgument.Value == DayOfWeek.Friday);
        }
    }
}
