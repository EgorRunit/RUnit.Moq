using Moq;
using RUnit.Test.TestClasses;
using System.Text;

namespace RUnit.Test
{
    enum Kind
    {
        AtLeastOnce,
        AtLeast,
        AtMost,
        AtMostOnce,
        BetweenExclusive,
        BetweenInclusive,
        Exactly,
        Once,
        Never,
    }

    public readonly struct Timess
    {
        readonly Kind kind;

        Timess(Kind kind)
        {
            this.kind = kind;
        }

        public static Timess Once()
        {
            return new Timess(Kind.Once);
        }
    }

    public class DDDD
    {
        //public void Compare()
    }

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            var dd = Timess.Once;

            var mock = new Mock<TestClasses.ITest>();
            mock.Setup(x => x.Write2(new StringBuilder()))
                .Callback<string>((x) =>
                {
                    Console.WriteLine("Mock write string");
                }).Returns("|");

            mock.Object.Write1(new StringBuilder());
            mock.Verify(x=> x.Write1(new StringBuilder()), () => { return Times.Once(); });
        }
    }
}
