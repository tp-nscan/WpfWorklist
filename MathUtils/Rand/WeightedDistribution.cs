using System;
using System.Collections;
using System.Collections.Generic;

namespace MathUtils.Rand
{
    public class WeightedDistribution<T> : IEnumerable<Tuple<double, T>>
    {
        public WeightedDistribution(int seed, IEnumerable<Tuple<double, T>> weightedItems)
        {
            _seed = seed;
            _random = Randy.Fast(seed).ToDouble();

            CumulativeWeight = 0;
            foreach (var weightedItem in weightedItems)
            {
                CumulativeWeight += weightedItem.Item1;
                _bins.Add(new Tuple<double, T>(CumulativeWeight, weightedItem.Item2));
            }
        }

        readonly List<Tuple<double, T>> _bins = new List<Tuple<double, T>>();

        private readonly int _seed;
        public int Seed
        {
            get { return _seed; }
        }

        double CumulativeWeight { get; set; }

        T FindValue(double binValue)
        {
            for (var i=0; i<_bins.Count; i++)
            {
                if (binValue < _bins[i].Item1)
                {
                    return _bins[i].Item2;
                }
            }
            throw new Exception("Incorrect bin value");
        }

        private readonly IRandomDouble _random;
        public IEnumerable<T> DrawWeighted(int count, int seed)
        {
            for (var i = 0; i < count; i++ )
            {
                yield return FindValue(_random.Next() * CumulativeWeight);
            }
        }

        public IEnumerator<Tuple<double, T>> GetEnumerator()
        {
            return _bins.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
