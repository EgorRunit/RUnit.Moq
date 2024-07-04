using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace BUnit.Moq.Builders
{
    internal interface IInterfaceTypeBuilder
    {
        Type Build<T>(ModuleBuilder moduleBuilder, InterfaceMethodBuilder<T> mockMethodBuilder) where T : class;
    }
}
