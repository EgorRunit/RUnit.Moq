using BUnit.Moq.Setups;
using xAssert = Xunit.Assert;
using bAssert = BUnit.Moq.Assert;
using System.Text;

namespace BUnit.Test.Setups
{
    public class ReturnsFunctionsTest
    {


        [Fact]
        public void Return_ConstInt()
        {
            //arrange
            const int constValue1 = 2;
            const int constValue2 = 3;
            var argument1 = 0;
            var argument2 = 0;
            var callbackFunctions = new ReturnsFunction<ITestInterface, string>();

            //callbackFunctions.Returns

        }
    }
}

