using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace RUnIt.Moq
{
    public class ProxyMock<T> where T : class
    {
        Dictionary<string, Action> _actionHandlers = new Dictionary<string, Action>();
        Dictionary<string, Func<object>> _returnHandlers = new Dictionary<string, Func<object>>();

        //Здесь нужно проверять наличие _actionHandlers и _returnHandlers
        protected internal void Execute(string methodName, List<object> list)
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
