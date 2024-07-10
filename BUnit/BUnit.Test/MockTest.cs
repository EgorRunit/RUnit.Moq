using BUnit.Moq;
using BUnit.Moq.Setups;
using xAssert = Xunit.Assert;
using bAssert = BUnit.Moq.Assert;

namespace BUnit.Test
{
    public interface ITestInterface
    {
        void WriteVoid(int a, int b);
        int WriteInt(string a, string b);
    }

    public class MockTest
    {
        Mock<ITestInterface> _mock;

        public MockTest()
        {
            _mock = new Mock<ITestInterface>();
        }

        [Fact]
        public void MockObject_NotNull()
        {
            //assert
            xAssert.IsAssignableFrom<ITestInterface>(_mock.Object);
            bAssert.IsAssignableFrom<ITestInterface>(_mock.Object);
        }

        [Fact]
        public void Switch_Between_CallbackSetup_And_ReturnsSetup()
        {
            //arrange
            var setup1 = _mock.Setup(x => x.WriteVoid(It.Any<int>(), It.Any<int>()));
            var setup2 = _mock.Setup(x => x.WriteInt(It.Any<string>(), It.Any<string>()));

            //assert
            xAssert.IsAssignableFrom<CallbackSetup<ITestInterface>>(setup1);
            xAssert.IsAssignableFrom<ReturnsSetup<ITestInterface,int>>(setup2);
            bAssert.IsAssignableFrom<CallbackSetup<ITestInterface>>(setup1);
            bAssert.IsAssignableFrom<ReturnsSetup<ITestInterface, int>>(setup2);
        }

        [Fact]
        public void CallCallback_Action_Int_Int()
        {
            //arrange
            int value1 = 0;
            int value2 = 0;
            var callbackResult = _mock.Setup(x => x.WriteVoid(It.Any<int>(), It.Any<int>()))
               .Callback<int, int>((x1, x2) =>
               {
                   value1 = x1;
                   value2 = x2;
               });

            //act
            _mock.Object.WriteVoid(4, 5);

            //assert
            xAssert.Equal<int>(4, value1);
            xAssert.Equal<int>(5, value2);
            xAssert.IsType<CallbackResult>(callbackResult);
        }


        //[Fact]
        //public void CallCallback_Function_String_String()
        //{
        //    ////arrange
        //    //var value1 = "";
        //    //var value2 = "";
        //    //var setup = _mock.Setup(x => x.Write(It.Any<string>(), It.Any<string>()))
        //    //    .Callback<string, string>((x1, x2) =>
        //    //    {
        //    //        value1 = x1;
        //    //        value2 = x2;
        //    //    });

        //    ////act
        //    //_mock.Object.Write("1","2");

        //    ////assert
        //    //Assert.Equal("1", value1);
        //    //Assert.Equal("2", value2);
        //    //Assert.IsType<SetupSettingFunction>(setup);
        //}


        //[Fact]
        //public void Switch_SettingSetupAction_SettingSetupFunction2()
        //{
        //    ////arrange
        //    //var value1 = "";
        //    //var value2 = "";
        //    //var setup = _mock.Setup(x => x.Write(It.Any<string>(), It.Any<string>()))

        //    //    .Callback<string, string>((x1, x2) =>
        //    //    {
        //    //        value1 = x1;
        //    //        value2 = x2;
        //    //    });

        //    ////act
        //    //var rrr =_mock.Object.Write("1", "2");

        //    ////assert
        //    //Assert.IsType<SetupSettingFunction>(setup);
        //}
    }
}
