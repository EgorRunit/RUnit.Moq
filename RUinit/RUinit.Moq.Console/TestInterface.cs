using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUinit.Moq.Consoles
{
    public interface ITestInterface
    {
        void Write();
        void Write(string text, int i, StringBuilder sb);
        int WriteInt(int ddd);
    }

    public class TestInterface : ITestInterface
    {
        public int WriteInt(int dddd)
        {
            return 5;
        }
        public void Write()
        {
        }

        public void Write(string text, int i, StringBuilder sb)
        {

        }
    }
}
