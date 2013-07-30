namespace SortingNetwork.SorterPoolOpsSpec
{
    public class SorterPoolOpRandomGen
    {
        public static ISorterPoolOp Make(string comment, int keyCount, int seedIn, int seedOut, int switchesPerSorter, int sorterCount)
        {
            return new SorterPoolOpRandomGenImpl(comment, keyCount, seedIn, seedOut, switchesPerSorter, sorterCount);
        }
    }

    class SorterPoolOpRandomGenImpl : ISorterPoolOp
    {

        public SorterPoolOpRandomGenImpl(string comment, int keyCount, int seedIn, int seedOut, int switchesPerSorter, int sorterCount)
        {
            _comment = comment;
            _keyCount = keyCount;
            _seedIn = seedIn;
            _seedOut = seedOut;
            _switchesPerSorter = switchesPerSorter;
            _sorterCount = sorterCount;
        }

        public SorterPoolOpType SorterPoolOpType { get {return SorterPoolOpType.RandomGen;} }

        private readonly string _comment;
        public string Comment
        {
            get { return _comment; }
        }

        private readonly int _keyCount;
        public int KeyCount
        {
            get { return _keyCount; }
        }

        private readonly int _seedIn;
        public int SeedIn
        {
            get { return _seedIn; }
        }

        private readonly int _seedOut;
        public int SeedOut
        {
            get { return _seedOut; }
        }

        private readonly int _sorterCount;
        public int SorterCount
        {
            get { return _sorterCount; }
        }

        private readonly int _switchesPerSorter;
        public int SwitchesPerSorter
        {
            get { return _switchesPerSorter; }
        }
    }
}
