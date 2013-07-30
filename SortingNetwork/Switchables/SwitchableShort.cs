using System;
using System.Collections.Generic;
using MathUtils.Rand;
using MathUtils.SortableUtils;
using System.Linq;
using SortingNetwork.KeyPair;

namespace SortingNetwork.Switchables
{
    public interface ISwitchableShort : ISwitchable
    {
        ushort Value { get; }
    }

    public static class SwitchableShort
    {
        public static ISwitchableShort Make(ushort value)
        {
            return SwitchableShortImpl.FromShort(value);
        }

        public static IEnumerable<ISwitchableShort> MakeRandoms(IRandomBool random, int itemCount)
        {
            for (var i = 0; i < itemCount; i++)
            {
                yield return MakeRandom(random);
            }
        }

        public static ISwitchableShort MakeRandom(IRandomBool random)
        {
            return Make(Generators.RandomBits(16, random, 0.5).ToArray().ToUshort());
        }
    }

    class SwitchableShortImpl : ISwitchableShort
    {
        public static SwitchableShortImpl FromShort(ushort value)
        {
            return values[value];
        }

        SwitchableShortImpl(ushort value)
        {
            _value = value;
        }

        public SwitchableType SwitchableType { get { return SwitchableType.Short; } }

        private readonly ushort _value;
        public ushort Value
        {
            get { return _value; }
        }

        static readonly SwitchableShortImpl[] values = new SwitchableShortImpl[65536];

        static readonly bool[] isSortedMap = new bool[65536];
        static SwitchableShortImpl()
        {
            for (int i = 0; i < 65536; i++)
            {
                values[i] = new SwitchableShortImpl((ushort)i);
            }

            isSortedMap[0] = true;
            isSortedMap[32768] = true;
            isSortedMap[49152] = true;
            isSortedMap[57344] = true;
            isSortedMap[61440] = true;
            isSortedMap[63488] = true;
            isSortedMap[64512] = true;
            isSortedMap[65024] = true;
            isSortedMap[65280] = true;
            isSortedMap[65408] = true;
            isSortedMap[65472] = true;
            isSortedMap[65504] = true;
            isSortedMap[65520] = true;
            isSortedMap[65528] = true;
            isSortedMap[65532] = true;
            isSortedMap[65534] = true;
            isSortedMap[65535] = true;
        }

        public bool IsSorted { get { return isSortedMap[_value]; } }

        public IEnumerable<ISwitchable> CreateMutatedCopiesOf(IRando randomGen, int copyNumber)
        {
            for (var i = 0; i < copyNumber; i++)
            {
                yield return SwitchableShort.MakeRandom(randomGen.ToBool());
            }
        }

        public ISwitchable Switch(IKeyPair keyPair)
        {
            return ShortSwitchMaps.MapBySwitchKeys(keyPair)[_value];
        }

        public int KeyCount { get { return 16; } }
    }

    class ShortSwitchMaps
    {
        static ShortSwitchMaps()
        {
            var taggedSets = KeySet.Instance.AllPairsForKeyCount(16)
                                .Select(t => new Tuple<int, ISwitchableShort[]>(t.Index, MakeMap(t.LowKey, t.HiKey)))
                                    .ToList();

            foreach (var set in taggedSets.OrderBy(q=>q.Item1))
            {
                shortSwitches.Add(set.Item2);
            }
        }

        static readonly List<ISwitchableShort[]> shortSwitches = new List<ISwitchableShort[]>();

        public static ISwitchableShort[] MapBySwitchKeys(IKeyPair keyPair)
        {
            return shortSwitches[keyPair.Index];
        }

        static ISwitchableShort[] MakeMap(int lowBit, int hiBit)
        {
            var retMap = new ISwitchableShort[65536];
            for (var i = 0; i < 65536; i++)
            {
                retMap[i] = SwitchableShort.Make(((ushort)i).SortBitPair(lowBit, hiBit));
            }
            return retMap;
        }

    }
}
