using SortNetwork.KeySets;

namespace SorterGenomes.Common
{
    public class SorterGene : ISorterGene
    {
        public SorterGene(IKeyPair keyPair, int index)
        {
            _keyPair = keyPair;
            _index = index;
        }

        private readonly IKeyPair _keyPair;
        public IKeyPair KeyPair
        {
            get { return _keyPair; }
        }

        private readonly int _index;
        public int Index
        {
            get { return _index; }
        }

        public string Key { get { return KeyPair.ToLabel(); } }
    }
}
