using System;
using System.Collections.Generic;

namespace SortNetwork.KeySets
{
    public interface IKeyPair
    {
        int LowKey { get; }
        int HiKey { get; }
        int KeyCount { get; }
        int Index { get; }
        IKeyPair GetSibling(int index);
        int SiblingCount { get; }
    }

    public static class KeyPair
    {
        public static IKeyPair Make(int key1, int key2, int keyCount, int index, List<IKeyPair> siblings)
        {
            return new KeyPairImpl(key1, key2, keyCount, index, siblings);
        }

        public static bool Overlaps(this IKeyPair keyPair, IKeyPair otherKeyPair)
        {
            if (keyPair.LowKey == otherKeyPair.LowKey)
            {
                return true;
            }
            if (keyPair.LowKey == otherKeyPair.HiKey)
            {
                return true;
            }
            if (keyPair.HiKey == otherKeyPair.LowKey)
            {
                return true;
            }
            if (keyPair.HiKey == otherKeyPair.HiKey)
            {
                return true;
            }

            return false;
        }
    }

    class KeyPairImpl : IKeyPair
    {
        private readonly int _lowKey;
        private readonly int _hiKey;
        private readonly int _keyCount;
        private readonly int _index;

        public KeyPairImpl(int key1, int key2, int keyCount, int index, List<IKeyPair> siblings)
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
