using TEstConsole.Setups;

namespace TEstConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var mock = new Mock<IMockTestClass>();
            var setup1 = mock.Setup(x => x.Write(5));
            var setup2 = mock.Setup(x => x.Write(5, 5));

            setup1.Callback<int>(x=> { });
            setup2.Callback<int, int>((x1, x2) => { })
                ;
            

            //var setupSettingAction = new SetupSettingAction();
            //setupSettingAction.
            //var setupSettingFunction = new SetupSettingFunction<Mock<, int>();
            //var setupSettingActionFunction = setupSettingFunction.Returns
            Console.WriteLine("Hello, World!");
        }
    }

    public interface IMockTestClass
    {
        void Write(int i1);
        int Write(int i1, int i2);
    }

    public class MockTestClass : IMockTestClass
    {
        public void Write(int i1)
        {
            throw new NotImplementedException();
        }

        public int Write(int i1, int i2)
        {
            throw new NotImplementedException();
        }
    }
}


