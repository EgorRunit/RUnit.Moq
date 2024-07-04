using BUnit.Moq;
using BUnit.Moq.Builders;
using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace BUnit.Consoles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _ittMock = new Mock<ITT>();
            var sb = new StringBuilder("eeeeeee");
            _ittMock.Setup(x => x.Write("d", sb)).Callback<string, StringBuilder>((x1, x2) =>
            {

                Console.WriteLine("Перегрузка2");
                Console.WriteLine(x1);
                Console.WriteLine(x2);
            });
            //_ittMock.Setup(x => x.Write("d", It.Any<StringBuilder>())).Callback<string,StringBuilder>((x1,x2)=>
            //{
               
            //    Console.WriteLine("Перегрузка2");
            //    Console.WriteLine(x1);
            //    Console.WriteLine(x2);
            //});

            

            var sss = _ittMock.Object;
            //sss.Write(4);
            //sss.Write("Ddd");
            sss.Write("1111", new StringBuilder("ffff111111111111111"));


            

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

    public interface ITT
    {
        void Write(int s);
        int Write(string s);
        string Write(string s1, StringBuilder sb);

    }
}
