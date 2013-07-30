using System;
using System.Collections.Generic;
using System.Linq;

namespace MathUtils.Collections
{
    public static class CollectionExt
    {
        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> items) where T : class 
        {
            return items.Where(i => i != null);
        }

        public static K GetFirstNonMatching<K, V>(this IEnumerable<K> keys, IEnumerable<V> values, Func<V, K> keySelector)
        {
            var dict = values.ToDictionary(keySelector);
            foreach (var key in keys.Where(key => ! dict.ContainsKey(key)))
            {
                return key;
            }
            return default(K);
        }

        public static IEnumerable<T> RoundRobin<T>(this IEnumerable<T> items, int startPosition)
        {
            var itemList = items.ToList();
            while (true)
            {
                yield return itemList[startPosition++%itemList.Count];
            }
        }

        public static IEnumerable<Tuple<S, T>> OuterJoinWith<S, T>(this IEnumerable<S> sVals, IEnumerable<T> tVals) 
            where S: class 
            where T: class 
        {
            var sList = sVals.ToList();
            var tList = tVals.ToList();

            var maxLen = Math.Max(sList.Count, tList.Count());

            for (int i = 0; i < maxLen; i++)
            {
                yield return new Tuple<S, T>
                    (
                       (i < sList.Count) ? sList[i] : null,
                       (i < tList.Count) ? tList[i] : null
                    );
            }
        }

        public static IEnumerable<Tuple<S, T>> JoinWith<S, T>(this IEnumerable<S> sVals, IEnumerable<T> tVals)
        {
            var sList = sVals.ToList();
            var tList = tVals.ToList();
            if (sList.Count != tList.Count)
            {
                throw new Exception("Enumerations are not the same length");
            }
            return sList.Select((s, i) => new Tuple<S, T>(s, tList[i]));
        }


    }
}
