using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUnit.Test.TestClasses
{
    public interface ITest
    {
        void Write(int text);
        int Write1(StringBuilder sb);
    }

    public class Test : ITest
    {
        public void Write(int text)
        {
            Console.WriteLine(text);
        }

        public int Write1(StringBuilder sb)
        {
            Console.WriteLine(sb);
            return 1;
        }
    }
}
