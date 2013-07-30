using System.Collections.Generic;
using MathUtils.Repos;

namespace SortingNetwork.Sorters
{
    public interface ISorterRepo<out T> : IIndexedRepo<T> where T : ISorter
    {
        int KeyCount { get; }
    }

    public static class SorterRepo
    {
        public static ISorterRepo<T> ToSorterRepo<T>(this IEnumerable<T> sorters) where T : ISorter
        {
            return new SorterRepoImpl<T>(sorters);
        }
    }

    class SorterRepoImpl<T> : Repo<T>, ISorterRepo<T> where T : ISorter
    {
        private readonly int _keyCount = 0;

        public SorterRepoImpl(IEnumerable<T> items)
            : base(items)
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

        public int KeyCount
        {
            get { return _keyCount; }
        }
    }
}
