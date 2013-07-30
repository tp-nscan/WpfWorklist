using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.Collections;
using SortingNetwork.Switchables;
using SortingNetwork.Switches;

namespace SortingNetwork.Sorters
{
    public abstract class SorterBase<T> : ISorter<T> where T : ISwitch
    {
        protected SorterBase(Guid guid, IEnumerable<T> switches)
        {
            _guid = guid;
            _switches = switches.ToList();

            if (_switches.Count == 0) { return; }

#if SAFE_MODE

            SafeModeCheck();
#endif
            _keyCount = _switches[0].KeyPair.KeyCount;
        }

        void SafeModeCheck()
        {
            if (_switches.AreOutOfOrder(sw => sw.Index))
            {
                throw new ArgumentException("Switches are not sequentially indexed");
            }

            var keyCountGroups = this.Switches.GroupBy(T => T.KeyPair.KeyCount).ToList();
            if (keyCountGroups.Count != 1)
            {
                throw new Exception("switchRepos must all have the same KeyCount");
            }
        }

        private readonly Guid _guid;
        public Guid Guid
        {
            get { return _guid; }
        }

        private readonly int _keyCount;
        public int KeyCount
        {
            get { return _keyCount; }
        }

        public abstract ISwitchable Sort(ISwitchable switchable);
        public abstract SorterType SorterType { get; }

        IEnumerable<ISwitch> ISorter.Switches
        {
            get { return (IEnumerable<ISwitch>) Switches; }
        }

        private readonly List<T> _switches;
        public IEnumerable<T> Switches
        {
            get
            {
                for (var i = 0; i < _switches.Count; i++)
                {
                    yield return _switches[i];
                }
            }
        }
    }
}
