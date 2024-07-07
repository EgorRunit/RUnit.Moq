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
        //int Write(string a, string b);
    }

    public class MockTest
    {
        Mock<ITestInterface> _mock;

        public MockTest()
        {
            _mock = new Mock<ITestInterface>();
        }

        [Fact]
        public void Switch_SettingSetupAction_SettingSetupFunction()
        {
            //arrange
            var setup1 = _mock.Setup(x => x.Write(It.Any<int>(), It.Any<int>()));
            //var setup2 = _mock.Setup(x => x.Write(It.Any<string>(), It.Any<string>()));

            //assert
            Assert.IsType<SetupSettingAction>(setup1);
            //Assert.IsType<SetupSettingFunction>(setup2);
        }

        [Fact]
        public void Switch_SettingSetupAction_SettingSetupFunction2()
        {
            //arrange
            var setup = _mock.Setup(x => x.Write(It.Any<int>(), It.Any<int>()))
                .Callback<int,int>((x1,x2) =>
                {
                });

            _mock.Object.Write(4, 5);

            //assert
            Assert.IsType<SetupSettingAction>(setup);
        }
    }
}
