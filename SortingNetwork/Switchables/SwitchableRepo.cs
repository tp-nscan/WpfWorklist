using System.Collections.Generic;
using MathUtils.Rand;
using MathUtils.Repos;

namespace SortingNetwork.Switchables
{
    public static class SwitchableRepo
    {
        public static ISwitchableRepo<T> ToRepo<T>(this IEnumerable<T> items) where T : ISwitchable
        {
            return new SwitchableRepoImpl<T>(items);
        }

        public static ISwitchableRepo<T> ToRandomDrawRepo<T>
            (
                this IEnumerable<T> items,
                int seed,
                int itemCount
            ) where T : ISwitchable
        {
            return items.RandomDraw(Randy.Fast(seed).ToInt(), itemCount).ToRepo();
        }

        public static ISwitchableRepo<T> ToRandomDrawRepo<T>
            (
                this IEnumerable<T> items,
                IRandomInt random,
                int itemCount
            ) where T : ISwitchable
        {
            return items.RandomDraw(random, itemCount).ToRepo();
        }
    }

    class SwitchableRepoImpl<T> : Repo<T>, ISwitchableRepo<T> where T : ISwitchable
    {
        public SwitchableRepoImpl(IEnumerable<T> switchables)
            : base(switchables)
        {
            if (Size == 0) { return; }

#if SAFE_MODE
            var keyCountGroups = Items.GroupBy(T => T.KeyCount).ToList();
            if (keyCountGroups.Count != 1)
            {
                throw new Exception("switchRepos must all have the same KeyCount");
            }
#endif
            _keyCount = this[0].KeyCount;
        }

        private readonly int _keyCount;
        public int KeyCount
        {
            get { return _keyCount; }
        }
    }
}
