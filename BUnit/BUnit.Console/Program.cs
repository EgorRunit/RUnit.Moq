using BUnit.Moq;
using BUnit.Moq.Builders;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace BUnit.Consoles
{
    //public class Test : ProxyMock<IT>
    //{
    //    internal void G(string str, List<object> list, ProxyMock proxyMock)
    //    {
    //    }

    //    internal void DDD()
    //    {
    //        var list = new List<object>();
    //        list.Add(5);
    //        list.Add(new StringBuilder());
    //        G("reeee", list, this);
            
    //    }
    //}

    public  interface IT
    {
        void G();
    }
    internal class Z
    {
        public void G()
        {

        }
    }

    internal class Vehicle
    {
        public void Go() => Console.WriteLine("Vehicle");
    }
    internal class Car:Vehicle
    {
        public new void Go() => Console.WriteLine("Car");
    }

    internal class Program: Z, IT
    {
        public static void NotNull([NotNull]object? obj)
        {
            if (obj == null)
            {
                throw new Exception("eeee");
            }
        }
        static void Main(string[] args)
        {

            return;
            //var _ittMock = new Mock<ITT>();
            //_ittMock.Verify(x => x.Write(It.Any<int>()));
            //var sb = new StringBuilder("eeeeeee");
            //_ittMock.Setup(x => x.Write("d", sb)).Callback<string, StringBuilder>((x1, x2) =>
            //{

            //    Console.WriteLine("Перегрузка2");
            //    Console.WriteLine(x1);
            //    Console.WriteLine(x2);
            //});
            //_ittMock.Setup(x => x.Write("d", It.Any<StringBuilder>())).Callback<string,StringBuilder>((x1,x2)=>
            //{

            //    Console.WriteLine("Перегрузка2");
            //    Console.WriteLine(x1);
            //    Console.WriteLine(x2);
            //});



            //var sss = _ittMock.Object;
            ////sss.Write(4);
            ////sss.Write("Ddd");
            //sss.Write("1111", new StringBuilder("ffff111111111111111"));




            //var assemblyName = new AssemblyName("DynamicAssemblyUnitTest");
            //_assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            //_moduleBuilder = _assemblyBuilder.DefineDynamicModule(assemblyName.Name ?? "DynamicAssemblyUnitTest");
            //InterfaceMethodBuilder<ITT> methodBuilder = new InterfaceMethodBuilder<ITT>();
            //var d = new InterfaceTypeBuilder();
            //var type = d.Build<ITT>(_moduleBuilder, methodBuilder);
            //var createdType = Activator.CreateInstance(type) as ITT;
            //var i = 1;
            //createdType.Write("f");
            //createdType.Write("dd", "ddd");

        }
    }

    public static class SetupActionExtrension
    {
    }

    public class SetupAction
    {

    }

    public class SetupFunction
    {

    }

    public interface ITT
    {
        void Write(int s);
        int Write(string s);
        string Write(string s1, StringBuilder sb);

    }
}
