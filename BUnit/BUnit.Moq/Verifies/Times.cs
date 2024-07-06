using System;
using System.Collections.Generic;
using System.Text;

namespace BUnit.Moq.Verifies
{
    public struct Times
    {
        readonly TimeKind _timeKind;
        readonly int _min;
        readonly int _max;

        Times(TimeKind timeKind, int min, int max)
        {
            _timeKind = timeKind;
            _min = min;
            _max = max;
        }

        public static Times AtLeastOnce()
        {
            return new Times(TimeKind.AtLeastOnce, 1, int.MaxValue);
        }
    }
}
