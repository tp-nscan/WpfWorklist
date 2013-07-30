using System.Collections.Generic;
using System.Linq;
using SortNetwork.Switchables;

namespace SortNetwork.Diff
{
    public interface ISwitchableRepoDiff<T> where T : ISwitchable
    {
        IEnumerable<ISwitchableDiff<T>> SwitchableDiffs { get; }
        bool HasDifferences { get; }
    }

    public static class SwitchableRepoDiff
    {
        public static ISwitchableRepoDiff<T> Make<T>(IEnumerable<T> poolA, IEnumerable<T> poolB) where T : ISwitchable
        {
            return new SwitchableRepoDiff<T>(poolA, poolB);
        }

    }

    public class SwitchableRepoDiff<T> : ISwitchableRepoDiff<T> where T : ISwitchable
    {
        public SwitchableRepoDiff(IEnumerable<T> poolA, IEnumerable<T> poolB)
        {
            foreach (var switchable in poolA)
            {
                var hc = switchable.GetHashCode();
                if (! _switchableDiffs.ContainsKey(hc))
                {
                    _switchableDiffs.Add(hc, new SwitchableDiffImpl<T>(switchable));
                }
                _switchableDiffs[hc].Acount++;
            }

            foreach (var switchable in poolB)
            {
                var hc = switchable.GetHashCode();
                if (!_switchableDiffs.ContainsKey(hc))
                {
                    _switchableDiffs.Add(hc, new SwitchableDiffImpl<T>(switchable));
                }
                _switchableDiffs[hc].Bcount++;
            }
        }

        private readonly Dictionary<int, ISwitchableDiff<T>> _switchableDiffs
            = new Dictionary<int, ISwitchableDiff<T>>();

        public IEnumerable<ISwitchableDiff<T>> SwitchableDiffs
        {
            get { return _switchableDiffs.Values; }
        }

        public bool HasDifferences
        {
            get { return SwitchableDiffs.Any(T=>T.IsDifferent); }
        }
    }

    public interface ISwitchableDiff<T> where T : ISwitchable
    {
        T Switchable { get; }
        int Acount { get; set; }
        int Bcount { get; set; }
        bool IsDifferent { get; }
    }

    public static class SwitchableDiff
    {
        public static ISwitchableDiff<T> Make<T>(T switchable) where T : ISwitchable
        {
            return new SwitchableDiffImpl<T>(switchable);
        }
    }

    class SwitchableDiffImpl<T> : ISwitchableDiff<T> where T : ISwitchable
    {
        public SwitchableDiffImpl(T switchable)
        {
            _switchable = switchable;
        }

        private readonly T _switchable;
        public T Switchable
        {
            get { return _switchable; }
        }

        public int Acount { get; set; }

        public int Bcount { get; set; }

        public bool IsDifferent
        {
            get { return Acount != Bcount; }
        }
    }
}
