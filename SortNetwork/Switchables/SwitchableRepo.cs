using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MathUtils.Rand;

namespace SortNetwork.Switchables
{
    public interface ISwitchableRepo : IReadOnlyCollection<ISwitchable>
    {
        int KeyCount { get; }
    }

    public static class SwitchableRepo
    {
        public static ISwitchableRepo ToSwitchableRepo(this IEnumerable<ISwitchable> items)
        {
            return new SwitchableRepoImpl(items);
        }

        public static ISwitchableRepo RandomBitArray(int keyCount, IRando rando, int itemCount)
        {
            return SwitchableBitArray.MakeRandoms(keyCount, rando, itemCount).ToSwitchableRepo();
        }

        public static ISwitchableRepo RandomIntArray(int keyCount, IRando rando, int itemCount)
        {
            return SwitchableIntArray.MakeRandoms(keyCount, rando, itemCount).ToSwitchableRepo();
        }

        public static ISwitchableRepo RandomSwitchableShort(int keyCount, IRando rando, int itemCount)
        {
            return SwitchableShort.MakeRandoms(rando, itemCount).ToSwitchableRepo();
        }

    }

    class SwitchableRepoImpl : ReadOnlyCollection<ISwitchable>, ISwitchableRepo
    {
        public SwitchableRepoImpl(IEnumerable<ISwitchable> switchables)
            : base(switchables.ToList())
        {
#if SAFE_MODE
            var keyCountGroups = Items.GroupBy(T => T.KeyCount).ToList();
            if (keyCountGroups.Count != 1)
            {
                throw new Exception("switchRepos must all have the same KeyCount");
            }
#endif
        }

        public int KeyCount
        {
            get { return this.Any() ? this[0].KeyCount : 0; }
        }
    }
}
