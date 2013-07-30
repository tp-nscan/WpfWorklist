namespace SortingNetwork.Switches
{
    /// <summary>
    /// Compares two switches independently of index, and allows for null switches
    /// </summary>
    public class SwitchDiff
    {
        private readonly int _index;
        private readonly ISwitch _switchA;
        private readonly ISwitch _switchB;

        public SwitchDiff(int index, ISwitch switchA, ISwitch switchB)
        {
            _index = index;
            _switchA = switchA;
            _switchB = switchB;
        }

        public int Index
        {
            get { return _index; }
        }

        public ISwitch SwitchA
        {
            get { return _switchA; }
        }

        public ISwitch SwitchB
        {
            get { return _switchB; }
        }

        public bool AreDifferent
        {
            get
            {
                if (SwitchA == null || SwitchB == null)
                {
                    return true;
                }

                return _switchA.KeyPair != SwitchB.KeyPair;
            }
        }
    }
}
