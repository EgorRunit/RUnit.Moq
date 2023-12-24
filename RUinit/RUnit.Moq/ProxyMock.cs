using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace RUnIt.Moq
{
    public class ProxyMock<T> where T : class
    {
        Dictionary<string, Setup> _actionHandlers;
        //Dictionary<string, Func<object>> _returnHandlers = new Dictionary<string, Func<object>>();

        public ProxyMock()
        {
            _actionHandlers = new Dictionary<string, Setup>();
        }


        internal void AddCallback(Setup setup)
        {
            if(!_actionHandlers.ContainsKey(setup.FullMethosSignatire))
            {
                _actionHandlers.Add(setup.FullMethosSignatire, setup);
            }
        }


        //Здесь нужно проверять наличие _actionHandlers и _returnHandlers
        protected internal void Execute(string methodName, List<object> list)
        {
             
            Console.WriteLine(methodName);
            Console.WriteLine(_actionHandlers);
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
