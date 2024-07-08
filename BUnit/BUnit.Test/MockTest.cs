using BUnit.Interfaces;
using BUnit.Moq;
using BUnit.Moq.Setups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUnit.Test
{
    public interface ITestInterface
    {
        void Write(int a, int b);
        int Write(string a, string b);
    }

    public class MockTest
    {
        Mock<ITestInterface> _mock;

        public MockTest()
        {
            _mock = new Mock<ITestInterface>();
        }

        [Fact]
        public void Switch_Between_ISetup_ProxyMock_And_ISetup_ProxyMock_Int()
        {
            //arrange
            var setup1 = _mock.Setup(x => x.Write(It.Any<int>(), It.Any<int>()));
            var setup2 = _mock.Setup(x => x.Write(It.Any<string>(), It.Any<string>()));

            //assert
            Assert.IsAssignableFrom<ISetup<ProxyMock>>(setup1);
            Assert.IsAssignableFrom<ISetup<ProxyMock, int>>(setup2);
        }

        [Fact]
        public void CallCallback_Action_Int_Int()
        {
            //arrange
            int value1 = 0;
            int value2 = 0;
            //var setup = _mock.Setup(x => x.Write(It.Any<int>(), It.Any<int>()))
            //   .Callback<int, int>((x1, x2) =>
            //   {
            //       value1 = x1;
            //       value2 = x2;
            //   });

            //_mock.Object.Write(4, 5);

            //assert
            Assert.Equal<int>(4, value1);
            Assert.Equal<int>(5, value2);
            //Assert.IsType<SetupSettingAction>(setup);
        }


        [Fact]
        public void CallCallback_Function_String_String()
        {
            ////arrange
            //var value1 = "";
            //var value2 = "";
            //var setup = _mock.Setup(x => x.Write(It.Any<string>(), It.Any<string>()))
            //    .Callback<string, string>((x1, x2) =>
            //    {
            //        value1 = x1;
            //        value2 = x2;
            //    });

            ////act
            //_mock.Object.Write("1","2");

            ////assert
            //Assert.Equal("1", value1);
            //Assert.Equal("2", value2);
            //Assert.IsType<SetupSettingFunction>(setup);
        }


        [Fact]
        public void Switch_SettingSetupAction_SettingSetupFunction2()
        {
            ////arrange
            //var value1 = "";
            //var value2 = "";
            //var setup = _mock.Setup(x => x.Write(It.Any<string>(), It.Any<string>()))
                
            //    .Callback<string, string>((x1, x2) =>
            //    {
            //        value1 = x1;
            //        value2 = x2;
            //    });

            ////act
            //var rrr =_mock.Object.Write("1", "2");

            ////assert
            //Assert.IsType<SetupSettingFunction>(setup);
        }
    }
}
