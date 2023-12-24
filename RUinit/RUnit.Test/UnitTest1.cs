using Moq;
using RUnit.Test.TestClasses;
using System.Text;

namespace RUnit.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            var mock = new Mock<TestClasses.ITest>();
            mock.Setup(x => x.Write1(new StringBuilder()))
                .Callback<string>((x) =>
                {
                    Console.WriteLine("Mock write string");
                });

            mock.Object.Write1(new StringBuilder());
        }
    }
}
