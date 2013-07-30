using System;
using System.Collections.Generic;
using MathUtils.Rand;

namespace SortNetwork.KeySets
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

        public IEnumerable<IKeyPair> AllKeyPairsForKeyCount(int keyCount)
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
                    var keyPair = KeyPair.Make(lowKey, hiKey, keyCount, keyIndex++, keyPairSet);
                    keyPairSet.Add(keyPair);
                    _keyPairs[MakeKeyPairKey(lowKey, hiKey, keyCount)] = keyPair;
                }
            }

            _keyPairsForKeyCount[keyCount] = keyPairSet;
        }


        readonly Dictionary<int, List<IKeyPair>> _keyPairsForKeyCount = new Dictionary<int, List<IKeyPair>>(); 
        readonly Dictionary<Tuple<int,int,int>, IKeyPair> _keyPairs = new Dictionary<Tuple<int, int, int>, IKeyPair>(); 

    }

    public static class KeySetEx
    {
        public static string ToLabel(this IKeyPair keyPair)
        {
            return keyPair.LowKey.ToString("D2") + "_" + keyPair.HiKey.ToString("D2");
        }

        public static IKeyPair Mutate(this IKeyPair keyPair, IRandomInt random)
        {
            var newIndex = random.Next(keyPair.SiblingCount);
            while (newIndex == keyPair.Index)
            {
                newIndex = random.Next(keyPair.SiblingCount);
            }
            return keyPair.GetSibling(newIndex);
        }
    }
}
