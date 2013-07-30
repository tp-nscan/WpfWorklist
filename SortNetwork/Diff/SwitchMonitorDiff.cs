using SortNetwork.Results;
using SortNetwork.Sorters;

namespace SortNetwork.Diff
{
    public interface ISwitchResultDiff : ISwitchDiff
    {
        new ISwitchResult SwitchA { get; }
        new ISwitchResult SwitchB { get; }
        bool UsagesAreDifferent { get; }
    }

    /// <summary>
    /// Compares two switches independently of index, and allows for null switches
    /// </summary>
    public static class SwitchResultDiff
    {
        public static ISwitchResultDiff Make(int index, ISwitchResult switchA, ISwitchResult switchB)
        {
            return new SwitchResultDiffImpl(index, switchA, switchB);
        }
    }
    class SwitchResultDiffImpl : ISwitchResultDiff
    {

        private readonly int _index;
        private readonly ISwitchResult _switchA;
        private readonly ISwitchResult _switchB;

        public SwitchResultDiffImpl(int index, ISwitchResult switchA, ISwitchResult switchB)
        {
            _index = index;
            _switchA = switchA;
            _switchB = switchB;
        }

        public int Index
        {
            get { return _index; }
        }

        ISwitch ISwitchDiff.SwitchA
        {
            get { return SwitchA; }
        }

        ISwitch ISwitchDiff.SwitchB
        {
            get { return SwitchB; }
        }

        public ISwitchResult SwitchA
        {
            get { return _switchA; }
        }

        public ISwitchResult SwitchB
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

        public bool UsagesAreDifferent
        {
            get { return SwitchA.UseCount != SwitchB.UseCount; }
        }
    }
}
