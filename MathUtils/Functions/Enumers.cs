using System.Collections.Generic;
using System.Linq;

namespace MathUtils.Functions
{
    public static class Enumers
    {
        public static IEnumerable<T> AsEnumerable<T>(this T item)
        {
            yield return item;
        }

        public static IEnumerable<double> SucessiveDiffs(this IEnumerable<double> list)
        {
            double lastItem = double.NaN;
            foreach (var d in list)
            {
                if(double.IsNaN(lastItem))
                {
                    lastItem = d;
                    continue;
                }

                yield return d - lastItem;
                lastItem = d;
            }
        }

        public static double SmallestGap(this IEnumerable<double> values)
        {
            return values.OrderBy(T=>T).SucessiveDiffs().Min();
        }

        public static IEnumerable<double> LatticePoints(double start, double end, double planck)
        {
            var current = start;
            while (current <= end)
            {
                yield return current;
                current += planck;
            }
        }
    }
}
