namespace SortingNetwork.SorterPoolOpsSpec
{
    public class SorterPoolOpEvo : ISorterPoolOp
    {
        public SorterPoolOpEvo(string comment, int seed, int reproductionRate, int numGenerations)
        {
            _comment = comment;
            _seed = seed;
            _reproductionRate = reproductionRate;
            _numGenerations = numGenerations;
        }

        public SorterPoolOpType SorterPoolOpType { get {return SorterPoolOpType.Evo;} }

        private readonly string _comment;
        public string Comment
        {
            get { return _comment; }
        }

        private readonly int _numGenerations;
        public int NumGenerations
        {
            get { return _numGenerations; }
        }

        private readonly int _reproductionRate;
        public int ReproductionRate
        {
            get { return _reproductionRate; }
        }

        private readonly int _seed;
        public int Seed
        {
            get { return _seed; }
        }
    }
}
