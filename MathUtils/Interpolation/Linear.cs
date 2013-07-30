using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathUtils.Interpolation
{
    public class Linear
    {
        public static List<double> Interpolate(IList<double> xItems, IList<double> yItems, IList<double> breaks)
        {
            double[] interpolated = new double[breaks.Count];
            int id = 1;
            int x = 0;
            while (breaks[x] < xItems[0])
            {
                interpolated[x] = yItems[0];
                x++;
            }

            double p, w;
            // left border case - uphold the value
            for (int i = x; i < breaks.Count; i++)
            {
                while (breaks[i] > xItems[id])
                {
                    id++;
                    if (id > xItems.Count - 1)
                    {
                        id = xItems.Count - 1;
                        break;
                    }
                }

                System.Diagnostics.Debug.WriteLine(string.Format("i: {0}, id {1}", i, id));

                if (id <= xItems.Count - 1)
                {
                    if (id == xItems.Count - 1 && breaks[i] > xItems[id])
                    {

                        interpolated[i] = yItems[yItems.Count - 1];
                    }
                    else
                    {
                        w = xItems[id] - xItems[id - 1];
                        p = (breaks[i] - xItems[id - 1]) / w;
                        interpolated[i] = yItems[id - 1] + p * (yItems[id] - yItems[id - 1]);
                    }
                }
                else // right border case - uphold the value
                {
                    interpolated[i] = yItems[yItems.Count - 1];
                }

            }
            return interpolated.ToList();

        }
    }
}
