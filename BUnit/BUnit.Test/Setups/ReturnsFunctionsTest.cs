using BUnit.Moq.Setups;
using xAssert = Xunit.Assert;
using bAssert = BUnit.Moq.Assert;
using System.Text;

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

        [Fact]
        public void Execute_ConstInt()
        {
            //arrange
            const int constValue1 = 2;
            const int constValue2 = 3;
            var argument1 = 0;
            var argument2 = 0;
            var callbackFunctions = new ReturnsFunction<ITestInterface, string>();
            var callbackResult = callbackFunctions.Callback<int, int>((i1, i2) =>
            {
                argument1 = i1;
                argument2 = i2;
            });

            //act
            callbackFunctions.Execute(new List<object> { constValue1, constValue2 });

            //assert
            xAssert.IsType<ReturnsThrow<ITestInterface, string>>(callbackResult);
            xAssert.Equal(constValue1, argument1);
            xAssert.Equal(constValue2, argument2);
            bAssert.IsType<ReturnsThrow<ITestInterface, string>>(callbackResult);
            bAssert.Equal(constValue1, argument1);
            bAssert.Equal(constValue2, argument2);
        }

        [Fact]
        public void Execute_VarInt()
        {
            //arrange
            var varValue1 = 2;
            var varValue2 = 3;
            var argument1 = 0;
            var argument2 = 0;
            var callbackFunctions = new CallbackFunctions<ReturnsThrow<ITestInterface, string>>();
            var callbackResult = callbackFunctions.Callback<int, int>((i1, i2) =>
            {
                argument1 = i1;
                argument2 = i2;
            });

            //act
            callbackFunctions.Execute(new List<object> { varValue1, varValue2 });

            //assert
            xAssert.IsType<ReturnsThrow<ITestInterface, string>>(callbackResult);
            xAssert.Equal(varValue1, argument1);
            xAssert.Equal(varValue2, argument2);
            bAssert.IsType<ReturnsThrow<ITestInterface, string>>(callbackResult);
            bAssert.Equal(varValue1, argument1);
            bAssert.Equal(varValue2, argument2);
        }


        [Fact]
        public void Execute_ConstString()
        {
            //arrange
            const string constValue1 = "222";
            const string constValue2 = "333";
            var argument1 = "";
            var argument2 = "";
            var callbackFunctions = new CallbackFunctions<ReturnsThrow<ITestInterface, string>>();
            var callbackResult = callbackFunctions.Callback<string, string>((s1, s2) =>
            {
                argument1 = s1;
                argument2 = s2;
            });

            //act
            callbackFunctions.Execute(new List<object> { constValue1, constValue2 });

            //assert
            xAssert.IsType<ReturnsThrow<ITestInterface, string>>(callbackResult);
            xAssert.Equal(constValue1, argument1);
            xAssert.Equal(constValue2, argument2);
            bAssert.IsType<ReturnsThrow<ITestInterface, string>>(callbackResult);
            bAssert.Equal(constValue1, argument1);
            bAssert.Equal(constValue2, argument2);
        }

        [Fact]
        public void Execute_VarString()
        {
            //arrange
            var varValue1 = "222";
            var varValue2 = "333";
            var argument1 = "";
            var argument2 = "";
            var callbackFunctions = new CallbackFunctions<ReturnsThrow<ITestInterface, string>>();
            var callbackResult = callbackFunctions.Callback<string, string>((s1, s2) =>
            {
                argument1 = s1;
                argument2 = s2;
            });

            //act
            callbackFunctions.Execute(new List<object> { varValue1, varValue2 });

            //assert
            xAssert.IsType<ReturnsThrow<ITestInterface, string>>(callbackResult);
            xAssert.Equal(varValue1, argument1);
            xAssert.Equal(varValue2, argument2);
            bAssert.IsType<ReturnsThrow<ITestInterface, string>>(callbackResult);
            bAssert.Equal(varValue1, argument1);
            bAssert.Equal(varValue2, argument2);
        }

        [Fact]
        public void Execute_VarStringBuilder()
        {
            //arrange
            var varValue1 = new StringBuilder("1111");
            var varValue2 = new StringBuilder("2222");
            var argument1 = new StringBuilder();
            var argument2 = new StringBuilder();
            var callbackFunctions = new CallbackFunctions<ReturnsThrow<ITestInterface, string>>();
            var callbackResult = callbackFunctions.Callback<StringBuilder, StringBuilder>((sb1, sb2) =>
            {
                argument1 = sb1;
                argument2 = sb2;
            });

            //act
            callbackFunctions.Execute(new List<object> { varValue1, varValue2 });

            //assert
            xAssert.IsType<ReturnsThrow<ITestInterface, string>>(callbackResult);
            xAssert.Equal(varValue1, argument1);
            xAssert.Equal(varValue2, argument2);
            bAssert.Equal(varValue1, argument1);
            bAssert.IsType<ReturnsThrow<ITestInterface, string>>(callbackResult);
            bAssert.Equal(varValue2, argument2);
        }
    }
}

