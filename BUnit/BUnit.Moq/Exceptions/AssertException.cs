using System;

namespace BUnit.Moq.Exceptions
{
    /// <summary>
    /// The base assert exception class
    /// </summary>
    public class AssertException : Exception
    {
        readonly string stackTrace;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertException"/> class.
        /// </summary>
        /// <param name="userMessage">The user message to be displayed</param>
        public AssertException(string userMessage)
            : this(userMessage, (Exception)null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertException"/> class.
        /// </summary>
        /// <param name="userMessage">The user message to be displayed</param>
        /// <param name="innerException">The inner exception</param>
        public AssertException(string userMessage, Exception innerException)
            : base(userMessage, innerException)
        {
            UserMessage = userMessage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="innerException"/> class.
        /// </summary>
        /// <param name="userMessage">The user message to be displayed</param>
        /// <param name="stackTrace">The stack trace to be displayed</param>
        protected AssertException(string userMessage, string stackTrace)
            : this(userMessage)
        {
            this.stackTrace = stackTrace;
        }

        /// <summary>
        /// Gets a string representation of the frames on the call stack at the time the current exception was thrown.
        /// </summary>
        /// <returns>A string that describes the contents of the call stack, with the most recent method call appearing first.</returns>
        public override string StackTrace => stackTrace ?? base.StackTrace;

        /// <summary>
        /// Gets the user message
        /// </summary>
        public string UserMessage { get; protected set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            var className = GetType().ToString();
            var message = Message;
            var result = default(string);

            if (message == null || message.Length <= 0)
                result = className;
            else
                result = string.Format("{0}: {1}", className, message);

            var stackTrace = StackTrace;
            if (stackTrace != null)
                result = result + Environment.NewLine + stackTrace;

            return result;
        }
    }
}
