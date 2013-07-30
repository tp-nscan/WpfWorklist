using System;
using SortNetwork.Switchables;

namespace SortNetwork.TestData
{
    public static class TestConstants
    {
        public static int KeyCount = 16;
        public static int ResultTestCount = 10;
        public static int Seed = 123;
        public static int SorterCount = 20;
        public static int SwitchesPerSorter = 200;
        public static int SwitchableCount = 100;
        public static SwitchableType SwitchableType = SwitchableType.BitArray;
        public static Guid SorterGuid = Guid.NewGuid();
        public static Guid SorterResultGuid = Guid.NewGuid();
    }
}
