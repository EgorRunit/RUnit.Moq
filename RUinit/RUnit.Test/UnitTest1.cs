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














    public interface ISetupSettingAction
    {

    }


    public class SetupActionCallback<T> where T : class
    {
        public T SetupActionCallbackWrite()
        {
            return this as T;

            //return (T)Convert.ChangeType(this, typeof(T));
        }
    }

    public interface ISetupAction<T>
    {
        Delegate InnerCallback { get; set; }
    }
    public interface ISetupFunction : ISetupAction<ISetupFunction>
    {
        Func<object> Returns { get; set; }
    }

    public static class SetupActionExtension
    {
        public static TResult Callback<T1>(this ISetupAction<SetupAction> setupAction, T1 t1) 
        {
            setupAction.InnerCallback = () => (t1);
            return null;

        }

        public static TResult Callback<T1, T2, TResult>(this ISetupAction setupAction, T1 t1, T2 t2) where TResult : class
        {
            setupAction.InnerCallback = ( )=>(t1,t2);
            return null;
        }
    }







    public static class SetupFunctionExtension
    {
        public static void Returns<T1>(this ISetupAction setupAction, T1 t1)
        {
            setupAction.InnerCallback = () => (t1);
        }

        public static void Return<T1, T2>(this ISetupAction setupAction, T1 t1, T2 t2)
        {
            setupAction.InnerCallback = () => (t1, t2);
        }
    }


    public class SetupAction : ISetupAction<SetupAction>
    {
        public Delegate InnerCallback { get;set; }
    }

    public class SetupFunction : ISetupFunction
    {
        public Delegate InnerCallback { get; set; }
        public Func<object> Returns { get; set; }

        public void SetupActionWrite() { }
    }

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var action = new SetupAction();
            action.Callback<int,int, string>(4, 5);
            action.InnerCallback.DynamicInvoke();
            var func = new SetupFunction();
            func. .Callback<int,int>(4, 5).;
            //func.SetupActionCallbackWrite().SetupActionReturnWrite();


            var dd = Timess.Once;

            var mock = new Mock<TestClasses.ITest>();
            mock.Setup(x => x.Write(It.IsAny<int>()));
            mock.Setup(x => x.Write1(new StringBuilder()))

                
                .Callback<string>((x) =>
                {
                    Console.WriteLine("Mock write string");
                })
                
                .Returns(null);

            mock.Object.Write1(new StringBuilder());
            mock.Verify(x=> x.Write1(new StringBuilder()), () => { return Times.Once(); });
        }
    }
}
