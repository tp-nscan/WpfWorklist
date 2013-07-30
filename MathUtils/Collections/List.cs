using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.Rand;

namespace MathUtils.Collections
{
    public static class List
    {
        public static bool AreOutOfOrder<T>(this IList<T> list, Func<T, int> indexer)
        {
            return list.Where((t, i) => indexer(t) != i).Any();
        }

        public static IEnumerable<T> Scramble<T>(this IEnumerable<T> items, int seed)
        {
            var randy = Randy.Fast(seed).ToInt();
            var list = items.ToList();

            while (list.Any())
            {
                var dex = randy.Next(list.Count);
                yield return list[dex];
                list.RemoveAt(dex);
            }
        }

        public static bool IsOrdered<T>(this IEnumerable<T> source)
        {
            var comparer = Comparer<T>.Default;
            T previous = default(T);
            bool first = true;

            foreach (T element in source)
            {
                if (!first && comparer.Compare(previous, element) > 0)
                {
                    return false;
                }
                first = false;
                previous = element;
            }
            return true;
        }

        public static void Recombine<T>(IEnumerable<T> aIn, IEnumerable<T> bIn, IEnumerable<bool> swaps, out List<T> aOut, out List<T> bOut)
        {
            var aList = aIn.ToList();
            var bList = bIn.ToList();
            var swapList = swaps.ToList();
            aOut = new List<T>();
            bOut = new List<T>();

            var ct = aList.Count;
            if (ct < 2)
            {
                throw new Exception("arrays must be length 2 or more");
            }
            if ((bList.Count != ct) || (swapList.Count != ct))
            {
                throw new Exception("arrays are not the same length");
            }
            for (var i = 0; i < ct; i++)
            {
                Set.SwapIf(ref aList, ref bList, swapList[i]);
                aOut.Add(aList[i]);
                bOut.Add(bList[i]);
            }
        }

        public static IEnumerable<T> Repeat<T>(this IEnumerable<T> original, int repeatCount)
        {
            var origList = original as IList<T> ?? original.ToList();

            for (var i = 0; i < repeatCount; i++)
            {
                for (var j = 0; j < origList.Count(); j++)
                {
                    yield return origList[j];
                }
            }
        }

        public static IEnumerable<T> ReadRange<T>(this IList<T> list, int start, int count)
        {
            for (var i = start; i < start + count; i++)
            {
                yield return list[i];
            }
        }

        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> enumerT, int chunkSize)
        {
            var retChunk = new List<T>();
            foreach (var t in enumerT)
            {
                retChunk.Add(t);
                if (retChunk.Count != chunkSize) continue;
                yield return retChunk;
                retChunk = new List<T>();
            }
        }

        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> enumerT, int chunkSize, int chunkCount)
        {
            var curChunk = 0;
            var retChunk = new List<T>();
            foreach (var t in enumerT)
            {
                retChunk.Add(t);
                if (retChunk.Count != chunkSize) continue;
                yield return retChunk;
                if (++curChunk == chunkCount) yield break;
                retChunk = new List<T>();
            }
        }

        public static IEnumerable<IEnumerable<Tuple<TS,T>>> DoubleChunk<TS,T>(this IEnumerable<TS> enumerableS, IEnumerable<T> enumerableT, int chunkSize)
        {
            var enumeratorS = enumerableS.GetEnumerator();
            var enumeratorT = enumerableT.GetEnumerator();
            var retChunk = new List<Tuple<TS, T>>();
            while (true)
            {
                enumeratorS.MoveNext();
                enumeratorT.MoveNext();
                retChunk.Add(new Tuple<TS, T>(enumeratorS.Current, enumeratorT.Current));
                if (retChunk.Count != chunkSize) continue;
                yield return retChunk;
                retChunk = new List<Tuple<TS, T>>();
            }
// ReSharper disable FunctionNeverReturns
        }
// ReSharper restore FunctionNeverReturns

        public static IEnumerable<IEnumerable<Tuple<TS, T>>> DoubleChunk<TS, T>(this IEnumerable<TS> enumerableS, IEnumerable<T> enumerableT, int chunkSize, int chunkCount)
        {
            var enumeratorS = enumerableS.GetEnumerator();
            var enumeratorT = enumerableT.GetEnumerator();
            var retChunk = new List<Tuple<TS, T>>();
            for (int i = 0; i < chunkCount; i++)
            {
                enumeratorS.MoveNext();
                enumeratorT.MoveNext();
                retChunk.Add(new Tuple<TS, T>(enumeratorS.Current, enumeratorT.Current));
                if (retChunk.Count != chunkSize) continue;
                yield return retChunk;
                retChunk = new List<Tuple<TS, T>>();
            }
        }

    }
}
