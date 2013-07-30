using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.Rand;
using MathUtils.SortableUtils;
using SortingNetwork.KeyPair;

namespace SortingNetwork.Switchables
{
    public interface ISwitchableBitArray : ISwitchable
    {
        bool Value(int index);
        bool this[int dex] { get; }
        IEnumerable<bool> Values { get; }
    }

    public static class SwitchableBitArray
    {
        public static ISwitchableBitArray Make(IEnumerable<bool> bits, int keyCount)
        {
            return new SwitchableBitArrayImpl(bits, keyCount);
        }

        public static IEnumerable<ISwitchableBitArray> MakeRandoms(int keyCount, IRandomBool random, int itemCount)
        {
            for (var i = 0; i < itemCount; i++)
            {
                yield return Make(Generators.RandomBits(keyCount, random, 0.5).Take(keyCount), keyCount);
            }
        }

        public static ISwitchableBitArray MakeRandom(int keyCount, IRandomBool random)
        {
            return Make(Generators.RandomBits(keyCount, random, 0.5).Take(keyCount), keyCount);
        }

        public static IEnumerable<ISwitchableBitArray> AllForKeyCount(int keyCount)
        {
            return SortableBitArrayUtils.AllBitSetsOfLength(keyCount).Select(
                bits => new SwitchableBitArrayImpl(bits, keyCount));
        }
    }

    class SwitchableBitArrayImpl : ISwitchableBitArray
    {
        public SwitchableBitArrayImpl(IEnumerable<bool> bits, int keyCount)
        {
            _keyCount = keyCount;
            _bitValues = bits.ToArray();
            if (Values.Count() != KeyCount)
            {
                throw new ArgumentException("Array is wrong length");
            }
        }

        private readonly int _keyCount;
        public int KeyCount
        {
            get { return _keyCount; }
        }

        public SwitchableType SwitchableType { get {return SwitchableType.BitArray;}}

        private readonly bool[] _bitValues;
        public bool Value(int index)
        {
            return _bitValues[index]; 
        }

        private bool? _isSorted;
        public bool IsSorted
        {
            get
            {
                if (! _isSorted.HasValue)
                {
                    _isSorted = _bitValues.IsSorted(KeyCount);
                }
                return _isSorted.Value;
            }
        }

        public IEnumerable<ISwitchable> CreateMutatedCopiesOf(IRando randomGen, int copyNumber)
        {
            for (var i = 0; i < copyNumber; i++)
            {
                yield return SwitchableBitArray.MakeRandom(KeyCount, randomGen.ToBool());
            }
        }

        public ISwitchable Switch(IKeyPair keyPair)
        {
            if (! _bitValues[keyPair.LowKey] && _bitValues[keyPair.HiKey])
            {
                return this;
            }
            return SwitchableBitArray.Make (SwitchedValues(keyPair.LowKey, keyPair.HiKey) , KeyCount);
        }

        IEnumerable<bool> SwitchedValues(int lowBit, int hiBit)
        {
            for (var i = 0; i < lowBit; i++)
            {
                yield return _bitValues[i];
            }
            yield return _bitValues[hiBit];

            for (var i = lowBit + 1; i < hiBit; i++)
            {
                yield return _bitValues[i];
            }
            yield return _bitValues[lowBit];

            for (var i = hiBit + 1; i < KeyCount; i++)
            {
                yield return _bitValues[i];
            }
        }

        public bool this[int dex]
        {
            get { return _bitValues[dex]; }
        }

        public IEnumerable<bool> Values
        {
            get
            {
                for (var i = 0; i < KeyCount; i++)
                {
                    yield return _bitValues[i];
                }
            }
        }

        public bool Equals(SwitchableBitArrayImpl other)
        {
            if (ReferenceEquals(null, other)) return false;
            //if (ReferenceEquals(this, other)) return true;
            if (other.KeyCount != KeyCount) return false;

            for (var bitDex = 0; bitDex < KeyCount; bitDex++)
            {
                if (other[bitDex] != this[bitDex])
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SwitchableBitArrayImpl)obj);
        }

        public override int GetHashCode()
        {
            return _bitValues.GetHashCode();
        }

        //public static bool operator ==(SwitchableBitArray left, SwitchableBitArray right)
        //{
        //    return Equals(left, right);
        //}

        //public static bool operator !=(SwitchableBitArray left, SwitchableBitArray right)
        //{
        //    return !Equals(left, right);
        //}
    }
}
