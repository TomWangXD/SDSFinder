using System;

namespace SDSFinder.Testss.Tests.Mock
{
    internal static class MockData
    {
        internal static int GetRandomInt()
        {
            Random rand = new();
            return rand.Next(0, 99999);
        }
    }
}
