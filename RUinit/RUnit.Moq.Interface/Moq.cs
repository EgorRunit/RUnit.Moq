using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUnit.Moq.Interface
{
    public class Moq<T>
    {
        public Mock()
        {
            var type = TypeFactory.Get<T>(this);
            var createdType = Activator.CreateInstance(type) as T;
            if (createdType != null)
            {
                Object = createdType;
                _proxyMock = Object as ProxyMock<T>;
            }
            else
            {
                throw new Exception("Не удалось создать тип");
            }
        }

    }
}
