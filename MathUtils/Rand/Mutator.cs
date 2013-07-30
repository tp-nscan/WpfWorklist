using System.Collections.Generic;

namespace MathUtils.Rand
{
    public static class Mutator
    {
        /// <summary>
        /// Mutates the sourceValues by substituting an element from mutated values when the current mutateFlags value is 1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceValues"></param>
        /// <param name="mutatedValues"></param>
        /// <param name="mutateFlags"></param>
        /// <returns></returns>
        public static IEnumerable<T> MutateZip<T>(this IEnumerable<T> sourceValues, IEnumerable<T> mutatedValues, IEnumerable<bool> mutateFlags)
        {
            var sourceEnumer = sourceValues.GetEnumerator();
            var mutatedValuesEnumer = mutatedValues.GetEnumerator();
            var mutateFlagEnumer = mutateFlags.GetEnumerator();

            while (sourceEnumer.MoveNext() && mutatedValuesEnumer.MoveNext() && mutateFlagEnumer.MoveNext())
            {
                yield return mutateFlagEnumer.Current ? mutatedValuesEnumer.Current : sourceEnumer.Current;
            }
        }

        public static IEnumerable<T> Mutate<T>(this IEnumerable<T> sourceValues, IEnumerable<T> mutatedValues, IEnumerable<bool> mutateFlags)
        {
            var sourceEnumer = sourceValues.GetEnumerator();
            var mutatedValuesEnumer = mutatedValues.GetEnumerator();
            var mutateFlagEnumer = mutateFlags.GetEnumerator();

            while (sourceEnumer.MoveNext() && mutateFlagEnumer.MoveNext())
            {
                if (mutateFlagEnumer.Current)
                {
                    mutatedValuesEnumer.MoveNext();
                    yield return mutatedValuesEnumer.Current;
                }
                else
                {
                    yield return sourceEnumer.Current;
                }
            }
        }
    }
}
