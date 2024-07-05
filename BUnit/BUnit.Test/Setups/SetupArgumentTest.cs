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
            Expression<Action<int>> expressionAction = t => Write(5);
            var setupArgument = new SetupArgument(expressionAction.GetExpression(0));

            //setupArgument
            Assert.True(setupArgument.Type == typeof(int));
            Assert.True(setupArgument.SetupArgumentType == SetupArgumentType.Constant);
            Assert.True(Convert.ToInt32(setupArgument.Value) == 5);
        }

        [Fact]
        public void Expression_With_ValueType_Member()
        {
            //arrange
            var cn = 5;
            Expression<Action<int>> expressionAction = t => Write(cn);
            var setupArgument = new SetupArgument(expressionAction.GetExpression(0));

            //act
            cn = 6;

            //asser
            Assert.True(setupArgument.Type == typeof(int));
            Assert.True(setupArgument.SetupArgumentType == SetupArgumentType.MemberAccess);
            Assert.True(Convert.ToInt32(setupArgument.Value) == cn);
        }

        [Fact]
        public void Expression_With_ReferenceType_New()
        {
            //arrange
            Expression<Action<StringBuilder>> expressionAction = t => Write(new StringBuilder());
            var setupArgument = new SetupArgument(expressionAction.GetExpression(0));

            //asser
            Assert.True(setupArgument.Type == typeof(StringBuilder));
            Assert.True(setupArgument.SetupArgumentType == SetupArgumentType.AnyValue);
        }

        [Fact]
        public void Expression_With_ReferenceType_Member()
        {
            //arrange
            var sb = new StringBuilder();
            Expression<Action<StringBuilder>> expressionAction = t => Write(sb);
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
            Expression<Action<DayOfWeek>> expressionAction = t => Write(DayOfWeek.Friday);
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
            Expression<Action<DayOfWeek>> expressionAction = t => Write(enumValue);
            var setupArgument = new SetupArgument(expressionAction.GetExpression(0));

            //asser
            Assert.True(setupArgument.Type == typeof(DayOfWeek));
            Assert.True(setupArgument.SetupArgumentType == SetupArgumentType.MemberAccess);
            Assert.True((DayOfWeek)setupArgument.Value == DayOfWeek.Friday);
        }
    }
}
