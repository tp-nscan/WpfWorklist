using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SortNetwork.Sorters
{
    public interface ISorterRepo : IReadOnlyCollection<ISorter>
    {
        int KeyCount { get; }
    }

    public static class SorterRepo
    {
        public static ISorterRepo ToSorterRepo(this IEnumerable<ISorter> sorters)
        {
            return new SorterRepoImpl(sorters);
        }
    }

    class SorterRepoImpl : ReadOnlyCollection<ISorter>, ISorterRepo
    {
        public SorterRepoImpl(IEnumerable<ISorter> items)
            : base(items.ToList())
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
