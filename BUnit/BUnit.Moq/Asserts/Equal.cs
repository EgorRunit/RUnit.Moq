using BUnit.Moq.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BUnit.Moq
{
    public partial class Assert
    {
        /// <summary>
        /// Verifies that two strings are equivalent.
        /// </summary>
        /// <param name="expected">The expected string value.</param>
        /// <param name="actual">The actual string value.</param>
        /// <exception cref="EqualException">Thrown when the strings are not equivalent.</exception>
        public static void Equal(string expected, string actual)
        {
            Equal(expected, actual, false, false, false);
        }

        /// <summary>
        /// Verifies that two strings are equivalent.
        /// </summary>
        /// <param name="expected">The expected string value.</param>
        /// <param name="actual">The actual string value.</param>
        /// <param name="ignoreCase">If set to <c>true</c>, ignores cases differences. The invariant culture is used.</param>
        /// <param name="ignoreLineEndingDifferences">If set to <c>true</c>, treats \r\n, \r, and \n as equivalent.</param>
        /// <param name="ignoreWhiteSpaceDifferences">If set to <c>true</c>, treats spaces and tabs (in any non-zero quantity) as equivalent.</param>
        /// <exception cref="EqualException">Thrown when the strings are not equivalent.</exception>
        public static void Equal(
            string expected,
            string actual,
            bool ignoreCase = false,
            bool ignoreLineEndingDifferences = false,
            bool ignoreWhiteSpaceDifferences = false)
        {

            // Start out assuming the one of the values is null
            int expectedIndex = -1;
            int actualIndex = -1;
            int expectedLength = 0;
            int actualLength = 0;

            if (expected == null)
            {
                if (actual == null)
                    return;
            }
            else if (actual != null)
            {
                expectedIndex = 0;
                actualIndex = 0;
                expectedLength = expected.Length;
                actualLength = actual.Length;

                while (expectedIndex < expectedLength && actualIndex < actualLength)
                {
                    char expectedChar = expected[expectedIndex];
                    char actualChar = actual[actualIndex];

                    if (ignoreLineEndingDifferences && IsLineEnding(expectedChar) && IsLineEnding(actualChar))
                    {
                        expectedIndex = SkipLineEnding(expected, expectedIndex);
                        actualIndex = SkipLineEnding(actual, actualIndex);
                    }
                    else if (ignoreWhiteSpaceDifferences && IsWhiteSpace(expectedChar) && IsWhiteSpace(actualChar))
                    {
                        expectedIndex = SkipWhitespace(expected, expectedIndex);
                        actualIndex = SkipWhitespace(actual, actualIndex);
                    }
                    else
                    {
                        if (ignoreCase)
                        {
                            expectedChar = Char.ToUpperInvariant(expectedChar);
                            actualChar = Char.ToUpperInvariant(actualChar);
                        }

                        if (expectedChar != actualChar)
                        {
                            break;
                        }

                        expectedIndex++;
                        actualIndex++;
                    }
                }
            }

            if (expectedIndex < expectedLength || actualIndex < actualLength)
            {
                throw new EqualException(expected, actual, expectedIndex, actualIndex);
            }
        }

        static bool IsLineEnding(char c)
        {
            return c == '\r' || c == '\n';
        }

        static bool IsWhiteSpace(char c)
        {
            return c == ' ' || c == '\t';
        }

        static int SkipLineEnding(string value, int index)
        {
            if (value[index] == '\r')
            {
                ++index;
            }
            if (index < value.Length && value[index] == '\n')
            {
                ++index;
            }

            return index;
        }

        static int SkipWhitespace(string value, int index)
        {
            while (index < value.Length)
            {
                switch (value[index])
                {
                    case ' ':
                    case '\t':
                        index++;
                        break;

                    default:
                        return index;
                }
            }

            return index;
        }

        /// <summary>
        /// Verifies that two arrays of unmanaged type T are equal, using Span&lt;T&gt;.SequenceEqual.
        /// </summary>
        /// <typeparam name="T">The type of items whose arrays are to be compared</typeparam>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <exception cref="EqualException">Thrown when the arrays are not equal</exception>
        /// <remarks>
        /// If Span&lt;T&gt;.SequenceEqual fails, a call to Assert.Equal(object, object) is made,
        /// to provide a more meaningful error message.
        /// </remarks>
        public static void Equal<T>(T[] expected, T[] actual)
            where T : unmanaged, IEquatable<T>
        {
            if (expected == null && actual == null)
                return;

            // Call into Equal<object> so we get proper formatting of the sequence
            if (expected == null || actual == null || !expected.AsSpan().SequenceEqual(actual))
                Equal<object>(expected, actual);
        }

        private static IEqualityComparer<T> GetEqualityComparer<T>(IEqualityComparer innerComparer = null)
        {
            return new AssertEqualityComparer<T>(innerComparer);
        }

        /// <summary>
        /// Verifies that two objects are equal, using a default comparer.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be compared</typeparam>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <exception cref="EqualException">Thrown when the objects are not equal</exception>
        public static void Equal<T>(T expected, T actual)
        {
            Equal(expected, actual, GetEqualityComparer<T>());
        }

        /// <summary>
        /// Verifies that two objects are equal, using a custom equatable comparer.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be compared</typeparam>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="comparer">The comparer used to compare the two objects</param>
        /// <exception cref="EqualException">Thrown when the objects are not equal</exception>
        public static void Equal<T>(T expected, T actual, IEqualityComparer<T> comparer)
        {
            //GuardArgumentNotNull(nameof(comparer), comparer);

            var expectedAsIEnum = expected as IEnumerable;
            var actualAsIEnum = actual as IEnumerable;
            var aec = comparer as AssertEqualityComparer<T>;

            // if we got an AssertEqualityComparer<T> we can invoke it to get the mismatched index.
            if (aec != null)
            {
                int? mismatchedIndex;

                if (!aec.Equals(expected, actual, out mismatchedIndex))
                {
                    if (mismatchedIndex.HasValue)
                        throw EqualException.FromEnumerable(expectedAsIEnum, actualAsIEnum, mismatchedIndex.Value);
                    else
                        throw new EqualException(expected, actual);
                }
            }
            else if (!comparer.Equals(expected, actual))
                throw new EqualException(expected, actual);
        }

        /// <summary>
        /// Verifies that two <see cref="double"/> values are equal, within the number of decimal
        /// places given by <paramref name="precision"/>. The values are rounded before comparison.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="precision">The number of decimal places (valid values: 0-15)</param>
        /// <exception cref="EqualException">Thrown when the values are not equal</exception>
        public static void Equal(double expected, double actual, int precision)
        {
            var expectedRounded = Math.Round(expected, precision);
            var actualRounded = Math.Round(actual, precision);

            if (!Object.Equals(expectedRounded, actualRounded))
                throw new EqualException(
                    string.Format(CultureInfo.CurrentCulture, "{0} (rounded from {1})", expectedRounded, expected),
                    string.Format(CultureInfo.CurrentCulture, "{0} (rounded from {1})", actualRounded, actual)
                );
        }

        /// <summary>
        /// Verifies that two <see cref="double"/> values are equal, within the number of decimal
        /// places given by <paramref name="precision"/>. The values are rounded before comparison.
        /// The rounding method to use is given by <paramref name="rounding" />
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="precision">The number of decimal places (valid values: 0-15)</param>
        /// <param name="rounding">Rounding method to use to process a number that is midway between two numbers</param>
        public static void Equal(double expected, double actual, int precision, MidpointRounding rounding)
        {
            var expectedRounded = Math.Round(expected, precision, rounding);
            var actualRounded = Math.Round(actual, precision, rounding);

            if (!Object.Equals(expectedRounded, actualRounded))
                throw new EqualException(
                    string.Format(CultureInfo.CurrentCulture, "{0} (rounded from {1})", expectedRounded, expected),
                    string.Format(CultureInfo.CurrentCulture, "{0} (rounded from {1})", actualRounded, actual)
                );
        }

        /// <summary>
        /// Verifies that two <see cref="double"/> values are equal, within the tolerance given by
        /// <paramref name="tolerance"/> (positive or negative).
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="tolerance">The allowed difference between values</param>
        /// <exception cref="ArgumentException">Thrown when supplied tolerance is invalid</exception>"
        /// <exception cref="EqualException">Thrown when the values are not equal</exception>
        public static void Equal(double expected, double actual, double tolerance)
        {
            if (double.IsNaN(tolerance) || double.IsNegativeInfinity(tolerance) || tolerance < 0.0)
                throw new ArgumentException("Tolerance must be greater than or equal to zero", nameof(tolerance));

            if (!(double.Equals(expected, actual) || Math.Abs(expected - actual) <= tolerance))
                throw new EqualException(
                    string.Format(CultureInfo.CurrentCulture, "{0:G17}", expected),
                    string.Format(CultureInfo.CurrentCulture, "{0:G17}", actual)
                );
        }

        /// <summary>
        /// Verifies that two <see cref="float"/> values are equal, within the tolerance given by
        /// <paramref name="tolerance"/> (positive or negative).
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="tolerance">The allowed difference between values</param>
        /// <exception cref="ArgumentException">Thrown when supplied tolerance is invalid</exception>"
        /// <exception cref="EqualException">Thrown when the values are not equal</exception>
        public static void Equal(float expected, float actual, float tolerance)
        {
            if (float.IsNaN(tolerance) || float.IsNegativeInfinity(tolerance) || tolerance < 0.0)
                throw new ArgumentException("Tolerance must be greater than or equal to zero", nameof(tolerance));

            if (!(float.Equals(expected, actual) || Math.Abs(expected - actual) <= tolerance))
                throw new EqualException(
                    string.Format(CultureInfo.CurrentCulture, "{0:G9}", expected),
                    string.Format(CultureInfo.CurrentCulture, "{0:G9}", actual)
                );
        }

        /// <summary>
        /// Verifies that two <see cref="decimal"/> values are equal, within the number of decimal
        /// places given by <paramref name="precision"/>. The values are rounded before comparison.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="precision">The number of decimal places (valid values: 0-28)</param>
        /// <exception cref="EqualException">Thrown when the values are not equal</exception>
        public static void Equal(decimal expected, decimal actual, int precision)
        {
            var expectedRounded = Math.Round(expected, precision);
            var actualRounded = Math.Round(actual, precision);

            if (expectedRounded != actualRounded)
                throw new EqualException(
                    string.Format(CultureInfo.CurrentCulture, "{0} (rounded from {1})", expectedRounded, expected),
                    string.Format(CultureInfo.CurrentCulture, "{0} (rounded from {1})", actualRounded, actual)
                );
        }

        /// <summary>
        /// Verifies that two <see cref="DateTime"/> values are equal, within the precision
        /// given by <paramref name="precision"/>.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="precision">The allowed difference in time where the two dates are considered equal</param>
        /// <exception cref="EqualException">Thrown when the values are not equal</exception>
        public static void Equal(DateTime expected, DateTime actual, TimeSpan precision)
        {
            var difference = (expected - actual).Duration();
            if (difference > precision)
            {
                throw new EqualException(
                    string.Format(CultureInfo.CurrentCulture, "{0} ", expected),
                    string.Format(CultureInfo.CurrentCulture, "{0} difference {1} is larger than {2}",
                        actual,
                        difference.ToString(),
                        precision.ToString()
                    ));
            }
        }

        /// <summary>
        /// Verifies that two objects are strictly equal, using the type's default comparer.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be compared</typeparam>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <exception cref="EqualException">Thrown when the objects are not equal</exception>
        public static void StrictEqual<T>(T expected, T actual) =>
            Equal(expected, actual, EqualityComparer<T>.Default);
    }
}
