using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.SortableUtils;
using MathUtils.Rand;
using SortNetwork.KeySets;

namespace SortNetwork.Switchables
{
    public interface ISwitchableIntArray : ISwitchable, IEquatable<ISwitchableIntArray>
    {
        IEnumerable<int> Values { get; }
        int Value(int index);
    }

    public class SwitchableIntArray
    {
        public static ISwitchableIntArray Make(IEnumerable<int> ints, int keyCount)
        {
            return new SwitchableIntArrayImpl(ints, keyCount);
        }

        public static IEnumerable<ISwitchableIntArray> MakeRandoms(int keyCount, IRando rando, int itemCount)
        {
            var random = rando.ToInt();
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
        public bool Equals(ISwitchableIntArray other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ISwitchableIntArray)) return false;
            return Equals((ISwitchableIntArray)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Values.Aggregate(1, (current, value) => (current*397) ^ value);
            }

            //var hashCode = 1;
            //foreach (var value in Values)
            //{
            //    hashCode = (hashCode * 397) ^ (value ? 3 : 1);
            //}
            //return hashCode;
        }

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
        public int Value(int index)
        {
            return _value[index]; 
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

        public string StringValue
        {
            get
            {
                return _value.Aggregate
                    (
                        string.Empty,
                        (s, bv) => s + (string.IsNullOrEmpty(s) ? string.Empty : ", ") + bv.ToString("D2")
                    );
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
