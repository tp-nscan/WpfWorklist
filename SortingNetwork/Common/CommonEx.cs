using System.Linq;
using MathUtils.Rand;
using SortingNetwork.KeyPair;
using SortingNetwork.Sorters;

namespace SortingNetwork.Common
{
    public static class CommonEx
    {
        #region formatting

        public static string ToLabel(this IKeyPair keyPair)
        {
            return keyPair.LowKey + "_" + keyPair.HiKey;
        }

        public static IKeyPair Mutate(this IKeyPair keyPair, IRandomInt random)
        {
            var newIndex = random.Next(keyPair.SiblingCount);
            while (newIndex == keyPair.Index)
            {
                newIndex = random.Next(keyPair.SiblingCount);
            }
            return keyPair.GetSibling(newIndex);
        }

        public static int ToHashCode(this ISorter sorter)
        {
            //var lps = sorter.Switches.Select(T => new Tuple<int, int>(T.Index, T.KeyPair.Index));
            return sorter.Switches.ToList()
                .Aggregate(1, (current, t) => current + (t.KeyPair.Index + t.Index + 1));
        }

        #endregion

    }
}
