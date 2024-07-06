using System;
using System.Collections.Generic;
using System.Text;

namespace BUnit.Moq.Verifies
{
    internal enum TimeKind
    {
        AtLeastOnce,
        AtLeast,
        AtMost,
        AtMostOnce,
        BetweenExclusive,
        BetweenInclusive,
        Exactly,
        Once,
        Never,
    }
}
