using System.Linq;

namespace MathUtils.SortableUtils
{
    public static class SortableIntArrayUtils
    {
        public static int CompareDict(this int[] sequence, int[] comp, int size)
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (sequence[i] != comp[i])
                {
                    return sequence[i] > comp[i] ? 1 : -1;
                }
            }
            return 0;
        }

        public static bool IsSorted(this int[] sequence, int size)
        {
            return !sequence.Where((t, i) => i != t).Any();
        }
    }
}
