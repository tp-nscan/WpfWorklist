using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUtils.VectorSpace
{
    public static class VectorSpaceExt
    {
        public static double DotProduct(this IEnumerable<double> vectorA, IEnumerable<double> vectorB)
        {
            var listA = vectorA.ToList();
            var listB = vectorB.ToList();
            if (listA.Count != listB.Count)
            {
                throw new Exception("vector lengths do not match");
            }
            return listA.Select((t, i) => t*listB[i]).Sum();
        }

        public static double DistanceSquared(this IEnumerable<double> vectorA, IEnumerable<double> vectorB)
        {
            var listA = vectorA.ToList();
            var listB = vectorB.ToList();
            if (listA.Count != listB.Count)
            {
                throw new Exception("vector lengths do not match");
            }
            return listA.Select((t, i) => (t - listB[i]) * (t - listB[i])).Sum();
        }
    }
}
