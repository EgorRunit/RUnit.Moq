//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TestConsole2.Test1
//{
//    internal class Callback<T>
//    {
//        public T Call()
//        {
//            var a = Activator.CreateInstance<T>();
//            Console.WriteLine($"Call - Type = {typeof(T)}");
//            return a;
//        }
//    }


//    public class CallbackResult {
//        public void CallbackTest()
//        {
//            Console.WriteLine("CallbackTest");
//        }
//    }
//    public class Returns
//    {
//        public int ReturnTest()
//        {
//            Console.WriteLine("ReturnTest");
//            return 10;
//        }
//    }

//    public class Test
//    {
//        public Test()
//        {
//            var callback1 = new Callback<CallbackResult>();
//            var callbackCall1 = callback1.Call();
//            //callbackCall1.CallbackTest();

//            var callback2 = new Callback<Returns>();
//            var callbackCall2 = callback2.Call();
//            var ssw = callbackCall2.ReturnTest() ;
//        }
//    }

//}
