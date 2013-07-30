using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.Repos;

namespace MathUtils.Rand
{
    public class RandomPartition<T>
    {
        public RandomPartition(IList<T> elements, IList<double> rankKeys, IList<int> partitions)
        {
            if (elements.Count != rankKeys.Count)
            {
                throw new Exception("elements list is wrong size");
            }

            var cume = 0;
            _partitionPoints = new List<int>();
            for (var i = 0; i < partitions.Count; i++)
            {
                cume += partitions[i];
                _partitionPoints.Add(cume);
            }

            if (elements.Count < cume)
            {
                throw new Exception("partition points are past the end of the list");
            }

            for (var i = 0; i < elements.Count; i++)
            {
                _rankableElements.Add(new Tuple<double, T>(rankKeys[i], elements[i]));
            }

            int curPartitionIndex = 0;
            int rankableIndex = 0;
            var curPartition = new List<T>();
            _partitionSets.Add(curPartition);
            foreach (var element in _rankableElements.OrderBy(T => T.Item1))
            {
                while ((curPartitionIndex < _partitionPoints.Count) && (_partitionPoints[curPartitionIndex] <= rankableIndex))
                {
                    curPartitionIndex++;
                    curPartition = new List<T>();
                    _partitionSets.Add(curPartition);
                }

                curPartition.Add(element.Item2);
                rankableIndex++;
            }
        }

        readonly List<List<T>> _partitionSets = new List<List<T>>();
        public IEnumerable<IList<T>> Partitions
        {
            get { return _partitionSets; }
        }

        readonly List<Tuple<double, T>> _rankableElements = new List<Tuple<double, T>>();
        readonly List<int> _partitionPoints;
    }

    //public class RandomPartition<T>
    //{
    //    public RandomPartition(IIndexedRepo<T> elements, IIndexedRepo<double> rankKeys, IIndexedRepo<int> partitions)
    //    {
    //        if (elements.Size != rankKeys.Size)
    //        {
    //            throw new Exception("elements list is wrong size");
    //        }

    //        var cume = 0;
    //        _partitionPoints = new List<int>();
    //        for (var i = 0; i < partitions.Size; i++)
    //        {
    //            cume += partitions[i];
    //            _partitionPoints.Add(cume);
    //        }

    //        if (elements.Size < cume)
    //        {
    //            throw new Exception("partition points are past the end of the list");
    //        }

    //        for (var i = 0; i < elements.Size; i++)
    //        {
    //            _rankableElements.Add(new Tuple<double, T>(rankKeys[i], elements[i]));
    //        }

    //        int curPartitionIndex = 0;
    //        int rankableIndex = 0;
    //        var curPartition = new List<T>();
    //        _partitionSets.Add(curPartition);
    //        foreach (var element in _rankableElements.OrderBy(T=>T.Item1))
    //        {
    //            while ((curPartitionIndex < _partitionPoints.Count) && (_partitionPoints[curPartitionIndex] <= rankableIndex))
    //            {
    //                curPartitionIndex++;
    //                curPartition = new List<T>();
    //                _partitionSets.Add(curPartition);
    //            }

    //            curPartition.Add(element.Item2);
    //            rankableIndex++;
    //        }
    //    }

    //    readonly List<List<T>> _partitionSets = new List<List<T>>();
    //    public IEnumerable<IList<T>> Partitions
    //    {
    //        get { return _partitionSets; }
    //    }

    //    readonly List<Tuple<double, T>> _rankableElements = new List<Tuple<double, T>>();
    //    readonly List<int> _partitionPoints;
    //}

}
