using TestConsole2.Setups;

namespace TestConsole2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var setupCallback = new CallbackSetup<ITestClass>();
            setupCallback .Callback(() => { Console.WriteLine("Вызов void callback"); });
            setupCallback.Execute(new List<object>());

            Console.WriteLine("----------------------------------------");

            var setupFunction = new ReturnsSetup<ITestClass, int>();
            var setupFunctionCallback = setupFunction.Callback<int>((x) => { Console.WriteLine($"Вызов int callback. Value = {x}"); });
            var setupFunctionValue = setupFunction.Returns(() => 5);
            //setupFunctionValue.
            //var setupFunctionCallback = setupFunction.Callback<int>((x) => { Console.WriteLine($"Вызов int callback. Value = {x}"); });
            
            //setupFunction.Execute(new List<object>() { 5});

            //var callback1 = new CallbackFunctions<CallbackResult>();
            //var callback2 = new CallbackFunctions<ReturnsResult>();
            //var callbackValue1 = callback1.Callback(() => { });
            //var callbackValue2 = callback2.Callback(() => { });
        }
    }

    public interface ITestClass
    {
        void Write();
        int WriteLine();

    }
}
