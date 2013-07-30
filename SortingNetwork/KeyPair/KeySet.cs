using System;
using System.Collections.Generic;

namespace SortingNetwork.KeyPair
{
    public class KeySet
    {
        private static readonly Lazy<KeySet> lazy = new Lazy<KeySet>(() => new KeySet());

        public static KeySet Instance { get { return lazy.Value; } }

        private KeySet() { }

        public IKeyPair GetKeyPair(int lowKey, int hiKey, int keyCount)
        {
            AddAllPairsForKeyCount(keyCount);
            return _keyPairs[MakeKeyPairKey(lowKey, hiKey, keyCount)];
        }

        static Tuple<int, int, int> MakeKeyPairKey(int lowKey, int hiKey, int keyCount)
        {
            return new Tuple<int, int, int>(lowKey, hiKey, keyCount);
        }

        public IEnumerable<IKeyPair> AllPairsForKeyCount(int keyCount)
        {
            AddAllPairsForKeyCount(keyCount);
            return _keyPairsForKeyCount[keyCount];
        }

        void AddAllPairsForKeyCount(int keyCount)
        {
            if (_keyPairsForKeyCount.ContainsKey(keyCount))
            {
                return;
            }
            var keyIndex = 0;
            var keyPairSet = new List<IKeyPair>();
            for (var lowKey = 0; lowKey < keyCount - 1; lowKey++)
            {
                for (var hiKey = lowKey + 1; hiKey < keyCount; hiKey++)
                {
                    var keyPair = new KeyPair(lowKey, hiKey, keyCount, keyIndex++, keyPairSet);
                    keyPairSet.Add(keyPair);
                    _keyPairs[MakeKeyPairKey(lowKey, hiKey, keyCount)] = keyPair;
                }
            }

            _keyPairsForKeyCount[keyCount] = keyPairSet;
        }

        readonly Dictionary<int, List<IKeyPair>> _keyPairsForKeyCount = new Dictionary<int, List<IKeyPair>>(); 
        readonly Dictionary<Tuple<int,int,int>, KeyPair> _keyPairs = new Dictionary<Tuple<int, int, int>, KeyPair>(); 

        class KeyPair : IKeyPair
        {
            private readonly int _lowKey;
            private readonly int _hiKey;
            private readonly int _keyCount;
            private readonly int _index;

            public KeyPair(int key1, int key2, int keyCount, int index, List<IKeyPair> siblings)
            {
                if (key1 == key2)
                {
                    throw new ArgumentException("keys cannot be equal");
                }

                if ((key1 >= keyCount) || (key2 >= keyCount))
                {
                    throw new ArgumentException("keys cannot be gte to KeyCount");
                }

                if (key1 < key2)
                {
                    _lowKey = key1;
                    _hiKey = key2;
                }
                else
                {
                    _lowKey = key2;
                    _hiKey = key1;
                }

                _keyCount = keyCount;
                _index = index;
                _siblings = siblings;
            }

            public int LowKey
            {
                get { return _lowKey; }
            }

            public int HiKey
            {
                get { return _hiKey; }
            }

            public int KeyCount
            {
                get { return _keyCount; }
            }

            public int Index
            {
                get { return _index; }
            }

            private readonly List<IKeyPair> _siblings; 
            public IKeyPair GetSibling(int index)
            {
                return _siblings[index];
            }

            public int SiblingCount
            {
                get
                {
                    return _siblings.Count;
                }
            }
        }
    }
}
