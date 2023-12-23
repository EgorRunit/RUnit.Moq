using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace RUnIt.Moq
{
    public class ProxyMock<T> where T : class
    {
        Dictionary<string, Action> _actions = new Dictionary<string, Action>();

        internal Mock<T>? Parent { get; set; }


        public void Print(string s, string s1, List<object> list = null)
        {
            Console.WriteLine(s);
            Console.WriteLine(s1);
            if(list != null)
            {
                foreach(var item in list)
                {
                    Console.WriteLine(item);
                }
            }
        }
        protected void Execute(string methodName, List<object> list)
        {
            Console.WriteLine(methodName);
            if (list != null)
            {
                foreach (var item in list)
                {
                    Console.WriteLine($"{item}={item.GetType()}");
                }
            }
            Console.WriteLine(this.GetHashCode());
        }
    }
}
