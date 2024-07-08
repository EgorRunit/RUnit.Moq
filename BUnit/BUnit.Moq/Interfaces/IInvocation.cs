using System;
using System.Collections.Generic;
using System.Reflection;

namespace BUnit.Interfaces
{
    public interface IInvocation
    {
        /// <summary>
        /// Gets the method of the invocation.
        /// </summary>
        MethodInfo Method { get; }

        /// <summary>
        /// Gets the arguments of the invocation.
        /// </summary>
        IReadOnlyList<object> Arguments { get; }

        /// <summary>
        ///   Gets the setup that matched this invocation (or <see langword="null"/> if there was no matching setup).
        /// </summary>
        //ISetup MatchingSetup { get; }

        /// <summary>
        ///   Gets whether this invocation was successfully verified by any of the various <c>`Verify`</c> methods.
        /// </summary>
        bool IsVerified { get; }

        /// <summary>
        /// The value being returned for a non-void method if no exception was thrown.
        /// </summary>
        object ReturnValue { get; }

        /// <summary>
        /// Optional exception if the method invocation results in an exception being thrown.
        /// </summary>
        Exception Exception { get; }
    }
}
