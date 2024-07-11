
namespace BUnit.Moq.Exceptions
{
    public class NotEqualException : AssertActualExpectedException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="NotEqualException"/> class.
        /// </summary>
        public NotEqualException(string expected, string actual)
            : base($"Not {expected ?? "(null)"}", actual ?? "(null)", "Assert.NotEqual() Failure")
        { }
    }

}
