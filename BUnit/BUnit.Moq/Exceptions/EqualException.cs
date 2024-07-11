using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BUnit.Moq.Exceptions
{
    //
    // Сводка:
    //     Exception thrown when two values are unexpectedly not equal.
    public class EqualException : AssertActualExpectedException
    {
        private static readonly Dictionary<char, string> Encodings = new Dictionary<char, string>
        {
            { '\r', "\\r" },
            { '\n', "\\n" },
            { '\t', "\\t" },
            { '\0', "\\0" }
        };

        private string message;

        //
        // Сводка:
        //     Gets the index into the actual value where the values first differed. Returns
        //     -1 if the difference index points were not provided.
        public int ActualIndex { get; }

        //
        // Сводка:
        //     Gets the index into the expected value where the values first differed. Returns
        //     -1 if the difference index points were not provided.
        public int ExpectedIndex { get; }

        public override string Message
        {
            get
            {
                if (message == null)
                {
                    message = CreateMessage();
                }

                return message;
            }
        }

        //
        // Сводка:
        //     Gets the index of the difference between the IEunmerables when converted to a
        //     string.
        public int? PointerPosition { get; private set; }

        //
        // Сводка:
        //     Creates a new instance of the Xunit.Sdk.EqualException class.
        //
        // Параметры:
        //   expected:
        //     The expected object value
        //
        //   actual:
        //     The actual object value
        public EqualException(object expected, object actual)
            : base(expected, actual, "Assert.Equal() Failure")
        {
            ActualIndex = -1;
            ExpectedIndex = -1;
        }

        //
        // Сводка:
        //     Creates a new instance of the Xunit.Sdk.EqualException class for string comparisons.
        //
        // Параметры:
        //   expected:
        //     The expected string value
        //
        //   actual:
        //     The actual string value
        //
        //   expectedIndex:
        //     The first index in the expected string where the strings differ
        //
        //   actualIndex:
        //     The first index in the actual string where the strings differ
        public EqualException(string expected, string actual, int expectedIndex, int actualIndex)
            : this(expected, actual, expectedIndex, actualIndex, null)
        {
        }

        private EqualException(string expected, string actual, int expectedIndex, int actualIndex, int? pointerPosition)
            : base(expected, actual, "Assert.Equal() Failure")
        {
            ActualIndex = actualIndex;
            ExpectedIndex = expectedIndex;
            PointerPosition = pointerPosition;
        }

        private string CreateMessage()
        {
            if (ExpectedIndex == -1)
            {
                return base.Message;
            }

            Tuple<string, string> tuple = ShortenAndEncode(base.Expected, PointerPosition ?? ExpectedIndex, '↓', ExpectedIndex);
            Tuple<string, string> tuple2 = ShortenAndEncode(base.Actual, PointerPosition ?? ActualIndex, '↑', ActualIndex);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.UserMessage);
            if (!string.IsNullOrWhiteSpace(tuple.Item2))
            {
                stringBuilder.AppendFormat(CultureInfo.CurrentCulture, "{0}          {1}", new object[2]
                {
                    Environment.NewLine,
                    tuple.Item2
                });
            }

            stringBuilder.AppendFormat(CultureInfo.CurrentCulture, "{0}Expected: {1}{0}Actual:   {2}", new object[3]
            {
                Environment.NewLine,
                tuple.Item1,
                tuple2.Item1
            });
            if (!string.IsNullOrWhiteSpace(tuple2.Item2))
            {
                stringBuilder.AppendFormat(CultureInfo.CurrentCulture, "{0}          {1}", new object[2]
                {
                    Environment.NewLine,
                    tuple2.Item2
                });
            }

            return stringBuilder.ToString();
        }

        //
        // Сводка:
        //     Creates a new instance of the Xunit.Sdk.EqualException class for IEnumerable
        //     comparisons.
        //
        // Параметры:
        //   expected:
        //     The expected object value
        //
        //   actual:
        //     The actual object value
        //
        //   mismatchIndex:
        //     The first index in the expected IEnumerable where the strings differ
        public static EqualException FromEnumerable(IEnumerable expected, IEnumerable actual, int mismatchIndex)
        {
            int? pointerPosition;
            string expected2 = ArgumentFormatter.Format(expected, out pointerPosition, mismatchIndex);
            int? pointerPosition2;
            string actual2 = ArgumentFormatter.Format(actual, out pointerPosition2, mismatchIndex);
            int? pointerPosition3 = (((pointerPosition ?? (-1)) > (pointerPosition2 ?? (-1))) ? pointerPosition : pointerPosition2);
            return new EqualException(expected2, actual2, mismatchIndex, mismatchIndex, pointerPosition3);
        }

        private static Tuple<string, string> ShortenAndEncode(string value, int position, char pointer, int? index = null)
        {
            if (value == null)
            {
                return Tuple.Create("(null)", "");
            }

            index = index ?? position;
            int num = Math.Max(position - 20, 0);
            int num2 = Math.Min(position + 41, value.Length);
            StringBuilder stringBuilder = new StringBuilder(100);
            StringBuilder stringBuilder2 = new StringBuilder(100);
            if (num > 0)
            {
                stringBuilder.Append("···");
                stringBuilder2.Append("   ");
            }

            for (int i = num; i < num2; i++)
            {
                char c = value[i];
                int repeatCount = 1;
                if (Encodings.TryGetValue(c, out var value2))
                {
                    stringBuilder.Append(value2);
                    repeatCount = value2.Length;
                }
                else
                {
                    stringBuilder.Append(c);
                }

                if (i < position)
                {
                    stringBuilder2.Append(' ', repeatCount);
                }
                else if (i == position)
                {
                    stringBuilder2.AppendFormat("{0} (pos {1})", new object[2] { pointer, index });
                }
            }

            if (value.Length == position)
            {
                stringBuilder2.AppendFormat("{0} (pos {1})", new object[2] { pointer, index });
            }

            if (num2 < value.Length)
            {
                stringBuilder.Append("···");
            }

            return Tuple.Create(stringBuilder.ToString(), stringBuilder2.ToString());
        }
    }
}
