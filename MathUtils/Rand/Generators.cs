using System;
using System.Collections.Generic;
using System.Linq;
using MathUtils.Interval;

namespace MathUtils.Rand
{
    public static class Generators
    {
        public static IEnumerable<bool> RandomBits(int count, int seed, double trueProbability)
        {
            if (! new RealInterval(0, 1).Contains(trueProbability))
            {
                throw new ArgumentException("trueProbability must be in unit interval");
            }

            var randomBool = Randy.Fast(seed).ToBool(trueProbability);

            for (var i = 0; i < count; i++)
            {
                yield return randomBool.Next();
            }
        }

        public static IEnumerable<bool> RandomBits(int count, IRandomBool randomBool, double trueProbability)
        {
            if (!new RealInterval(0, 1).Contains(trueProbability))
            {
                throw new ArgumentException("trueProbability must be in unit interval");
            }

            for (var i = 0; i < count; i++)
            {
                yield return randomBool.Next(trueProbability);
            }
        }

        public static IEnumerable<bool> RandomBits(int seed, double trueProbability)
        {
            if (!new RealInterval(0, 1).Contains(trueProbability))
            {
                throw new ArgumentException("trueProbability must be in unit interval");
            }

            var randomBool = Randy.Fast(seed).ToBool(trueProbability);

            while (true)
            {
                yield return randomBool.Next(trueProbability);
            }
        }

        public static IEnumerable<bool> RandomBits(IRandomBool randomBool, double trueProbability)
        {
            if (!new RealInterval(0, 1).Contains(trueProbability))
            {
                throw new ArgumentException("trueProbability must be in unit interval");
            }
            while (true)
            {
                yield return randomBool.Next(trueProbability);
            }
        }

        public static IEnumerable<T> RandomDraw<T>(this IEnumerable<T> pool, IRandomInt randomInt, int itemCount)
        {
            var poolList = pool.ToList();
            for (var i = 0; i < itemCount; i++)
            {
                yield return poolList[randomInt.Next(poolList.Count)];
            }
        }

        public static IEnumerable<T> RandomDraw<T>(this IEnumerable<T> pool, IRandomInt randomInt)
        {
            var poolList = pool.ToList();
            while (true)
            {
                yield return poolList[randomInt.Next(poolList.Count)];
            }
        }

        public static IEnumerable<double> Doubles(int count, int seed)
        {
            var randomDouble = Randy.Fast(seed).ToDouble();
            for (var i = 0; i < count; i++)
            {
                yield return randomDouble.Next();
            }
        }

        public static IEnumerable<double> ExpDist(int count, int seed, double max)
        {
            var logMax = Math.Log(max);
            var randomDouble = Randy.Fast(seed).ToDouble();
            for (var i = 0; i < count; i++)
            {
                yield return Math.Exp(randomDouble.Next()*logMax);
            }
        }

        public static IEnumerable<double> PowDist(int count, int seed, double max, double pow)
        {
            var randomDouble = Randy.Fast(seed).ToDouble();
            for (var i = 0; i < count; i++)
            {
                yield return Math.Pow(randomDouble.Next(), pow) * max;
            }
        }

        public static IEnumerable<int> Ints(int count, int seed)
        {
            var randomInt = Randy.Fast(seed).ToInt();
            for (var i = 0; i < count; i++)
            {
                yield return randomInt.Next();
            }
        }

        public static int[] IntegerPermutation(IRandomInt randy, int setCount)
        {
            var pool = Enumerable.Range(0, setCount).ToList();
            var result = new int[setCount];
            for (var count = setCount; count > 0; count--)
            {
                var pick = randy.Next(count);
                result[setCount-count] = pool[pick];
                pool.RemoveAt(pick);
            }
            return result;
        }
    }
}
