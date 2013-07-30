using System;
using System.Collections.Generic;
using System.Linq;

namespace MathUtils.Diff
{
    public class Hamming
    {
        public static double Distance<T>(List<T> leftVector, List<T> rightVector, Func<T, T, double> metric)
        {
            if (leftVector.Count != rightVector.Count)
            {
                throw new ArgumentException("Vectors must be the same length");
            }

            return leftVector.Select((t, i) => metric(t, rightVector[i])).Sum();
        }
    }
}
