using BUnit.Moq.Setups;
using xAssert = Xunit.Assert;
using bAssert = BUnit.Moq.Assert;

namespace BUnit.Test.Setups
{
    public class ReturnsFunctionsTest
    {
        [Fact]
        public void Return_ReturnsThrow()
        {
            //arrange
            var returnsFunction = new ReturnsFunction<ITestInterface, int>();

            //act
            var callbackResult = returnsFunction.Callback<int, int>((i1, i2) => { });

            //assert
            xAssert.IsType<ReturnsThrow<ITestInterface, int>>(callbackResult);
            bAssert.IsType<ReturnsThrow<ITestInterface, int>>(callbackResult);
        }
    }
}
