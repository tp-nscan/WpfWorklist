using System.Collections.Generic;
using System.Linq;
using MathUtils.Rand;

namespace MathUtils.Collections
{
    public static class Set
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            var c = a;
            a = b;
            b = c;
        }

        public static void SwapIf<T>(ref T a, ref T b, bool trigger)
        {
            if(! trigger) return;
            var c = a;
            a = b;
            b = c;
        }

        public static bool HasSameElementsAs<T>(this IEnumerable<T> list, IEnumerable<T> otherList)
        {
            var first = list as IList<T> ?? list.ToList();
            var second = otherList as IList<T> ?? otherList.ToList();
            return !first.Except(second).Any() && !second.Except(first).Any() && first.Count==second.Count;
        }

        public static bool HasSameElementsRepeatsAllowed<T>(this IEnumerable<T> list, IEnumerable<T> otherList)
        {
            var first = list as IList<T> ?? list.ToList();
            var second = otherList as IList<T> ?? otherList.ToList();
            return !first.Except(second).Any() && !second.Except(first).Any();
        }

        public static IEnumerable<T> RandomDrawWithoutReplacement<T>(this IEnumerable<T> drawPool, IRandomInt randy)
        {
            var drawList = drawPool.ToList();
            while (drawList.Count > 0)
            {
                var index = randy.Next(drawList.Count);
                var retVal = drawList[index];
                drawList.RemoveAt(index);
                yield return retVal;
            }
        }

        public static IEnumerable<T> RandomDrawWithReplacement<T>(this IEnumerable<T> drawPool, IRandomInt randy)
        {
            var drawList = drawPool.ToList();
            while (true)
            {
                yield return drawList[randy.Next(drawList.Count)];
            }
        }
    }
}
