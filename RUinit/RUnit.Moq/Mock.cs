using System.Linq.Expressions;
using RUnIt.Moq.Builders;

namespace RUnIt.Moq
{
    public class Mock<T> where T : class 
    {
        ProxyMock<T>? _proxyMock;
        Dictionary<string, Setup> _actions;
        
        public T Object { get; private set; }

        public Mock()
        {
            _actions= new Dictionary<string, Setup>();
            var type =TypeFactory.Get<T>(this);
            var createdType = Activator.CreateInstance(type) as T;
            if (createdType != null)
            {
                Object = createdType;
                _proxyMock = Object as ProxyMock<T>;
                if (_proxyMock != null)
                {
                    _proxyMock.Parent = this;
                }
            }
            else
            {
                throw new Exception("Не удалось создать тип");
            }
        }

        public Setup Setup(Expression<Action<T>> expression)
        {
            var type = Object.GetType();

            

           var expresionName = expression.Body.ToString();
            var setup = new Setup();
            if(_actions.ContainsKey(expresionName))
            {
                _actions[expresionName] = setup;
            }
            else
            {
                _actions.Add(expresionName, setup);
            }
            return setup;
        }
    }
}
