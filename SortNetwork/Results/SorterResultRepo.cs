using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SortNetwork.Results
{
    public interface ISorterResultRepo : IReadOnlyCollection<ISorterResult>
    {
        
    }

    public static class SorterResultRepo
    {
        public static ISorterResultRepo ToSorterResultRepo(this IEnumerable<ISorterResult> sorterResults)
        {
            return new SorterResultRepoImpl(sorterResults);
        }
    }

    class SorterResultRepoImpl : ReadOnlyCollection<ISorterResult>, ISorterResultRepo
    {
        public SorterResultRepoImpl(IEnumerable<ISorterResult> items)
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
            get { return this.Any() ? this.ElementAt(0).Sorter.KeyCount : 0; }
        }
    }
}
