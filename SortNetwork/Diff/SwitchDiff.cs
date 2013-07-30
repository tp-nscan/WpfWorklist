using SortNetwork.Sorters;

namespace SortNetwork.Diff
{
    public interface ISwitchDiff
    {
        int Index { get; }
        ISwitch SwitchA { get; }
        ISwitch SwitchB { get; }
        bool SwitchesAreDifferent { get; }
    }


    /// <summary>
    /// Compares two switches independently of index, and allows for null switches
    /// </summary>
    public static class SwitchDiff
    {
        public static ISwitchDiff Make(int index, ISwitch switchA, ISwitch switchB)
        {
            return new SwitchDiffImpl(index, switchA, switchB);
        }
    }
    class SwitchDiffImpl : ISwitchDiff
    {

        private readonly int _index;
        private readonly ISwitch _switchA;
        private readonly ISwitch _switchB;

        public SwitchDiffImpl(int index, ISwitch switchA, ISwitch switchB)
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

        public bool SwitchesAreDifferent
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
