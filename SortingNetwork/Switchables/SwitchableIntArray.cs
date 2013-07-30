using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.SortableUtils;
using MathUtils.Rand;
using SortingNetwork.KeyPair;

namespace SortingNetwork.Switchables
{
    public interface ISwitchableIntArray : ISwitchable
    {
        IEnumerable<int> Values { get; }
        int Value(int inddex);
    }

    public class SwitchableIntArray
    {
        public static ISwitchableIntArray Make(IEnumerable<int> ints, int keyCount)
        {
            return new SwitchableIntArrayImpl(ints, keyCount);
        }

        public static IEnumerable<ISwitchableIntArray> MakeRandoms(int keyCount, IRandomInt random, int itemCount)
        {
            for (var i = 0; i < itemCount; i++)
            {
                yield return MakeRandom(keyCount, random);
            }
        }

        public static ISwitchableIntArray MakeRandom(int keyCount, IRandomInt random)
        {
            return Make(Generators.IntegerPermutation(random, keyCount), keyCount);
        }
    }

    public class SwitchableIntArrayImpl : ISwitchableIntArray
    {
        public SwitchableIntArrayImpl(IEnumerable<int> value, int keyCount)
        {
            _keyCount = keyCount;
            _value = value.ToArray();
            if (Values.Count() != KeyCount)
            {
                throw new ArgumentException("Array is wrong length");
            }
        }

        public SwitchableType SwitchableType { get { return SwitchableType.IntArray; } }

        private readonly int[] _value;
        public int Value(int inddex)
        {
            return _value[inddex]; 
        }

        private bool? _isSorted;
        public bool IsSorted
        {
            get
            {
                if (! _isSorted.HasValue)
                {
                    _isSorted = _value.IsSorted(KeyCount);
                }
                return _isSorted.Value;
            }
        }

        public IEnumerable<ISwitchable> CreateMutatedCopiesOf(IRando randomGen, int copyNumber)
        {
            for (var i = 0; i < copyNumber; i++)
            {
                yield return SwitchableIntArray.MakeRandom(KeyCount, randomGen.ToInt());
            }
        }

        public ISwitchable Switch(IKeyPair keyPair)
        {
            return SwitchableIntArray.Make(SwitchedValues(keyPair.LowKey, keyPair.HiKey), KeyCount);
        }
        
        private readonly int _keyCount;
        public int KeyCount
        {
            get { return _keyCount; }
        }

        IEnumerable<int> SwitchedValues(int lowBit, int hiBit)
        {
            for (var i = 0; i < lowBit; i++)
            {
                yield return _value[i];
            }
            yield return _value[hiBit];

            for (var i = lowBit + 1; i < hiBit; i++)
            {
                yield return _value[i];
            }
            yield return _value[lowBit];

            for (var i = hiBit + 1; i < KeyCount; i++)
            {
                yield return _value[i];
            }
        }

        public IEnumerable<int> Values
        {
            get
            {
                for (var i = 0; i < KeyCount; i++)
                {
                    yield return _value[i];
                }
            }
        }
    }
}
