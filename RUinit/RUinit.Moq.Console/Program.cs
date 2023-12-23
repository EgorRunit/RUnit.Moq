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
            moq.Setup(x => x.Write("rrrrrrrrrrr", 44, new StringBuilder()))
                .Execute(() =>
                {
                    Console.WriteLine(s);
                });
            moq.Object.Write("arg1", 45, new StringBuilder("eeeeeee"));
            Console.ReadKey();
            return;
        }
    }
}
