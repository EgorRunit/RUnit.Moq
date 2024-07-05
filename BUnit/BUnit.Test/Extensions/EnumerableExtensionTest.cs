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
        public void Test()
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
        }
    }
}
