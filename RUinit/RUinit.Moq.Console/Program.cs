using RUnIt.Moq;
using System;
using System.Text;

namespace RUinit.Moq.Consoles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var moq = new Mock<ITestInterface>();
            var s = "Dddddddddd";
            moq.Setup(x => x.Write("rrrrrrrrrrr", It.Any<int>(), new StringBuilder("rrrrr")))
                .Callback(() =>
                {
                    Console.WriteLine(s);
                });
            moq.Setup(z => z.Write("Ssss", It.Any<int>(), new StringBuilder()))
                .Callback(() =>
                {
                    Console.WriteLine("2222222");
                });
            moq.Object.Write("arg1", 45, new StringBuilder("rrrrr"));
            Console.ReadKey();
            return;
        }
    }
}
