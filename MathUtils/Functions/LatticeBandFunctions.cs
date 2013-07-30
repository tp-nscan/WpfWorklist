using System;
using System.Collections.Generic;
using System.Linq;

namespace MathUtils.Functions
{
    public static class LatticeBandFunctions
    {
        public static List<List<T>> GetRowList<T>(this T[,] array)
        {
            var lstRet = new List<List<T>>();
            var rowCount = array.GetUpperBound(0) + 1;
            var colCount = array.GetUpperBound(1) + 1;

            for (var i = 0; i < rowCount; i++)
            {
                var lst = new List<T>();
                for(var j=0; j<colCount; j++)
                {
                    lst.Add(array[i,j]);
                }

                lstRet.Add(lst);
            }
            return lstRet;
        }

        public static List<List<T>> GetColumnList<T>(this T[,] array)
        {
            var lstRet = new List<List<T>>();
            var rowCount = array.GetUpperBound(0) + 1;
            var colCount = array.GetUpperBound(1) + 1;

            for (var i = 0; i < colCount; i++)
            {
                var lst = new List<T>();
                for (var j = 0; j < rowCount; j++)
                {
                    lst.Add(array[j, i]);
                }

                lstRet.Add(lst);
            }
            return lstRet;
        }

        public static double Purity(this IEnumerable<double> constituents)
        {
            if (constituents == null) return double.NaN;
            var list = constituents.ToList();
            if (Math.Abs(list.Sum() - 0) < double.Epsilon) return double.NaN;
            return list.Max() / list.Sum();
        }

        public static double Purity(this List<List<double>> constituents)
        {
            if (constituents == null) return double.NaN;
            var sum = constituents.Sum(T => T.Sum());
            if (Math.Abs(sum - 0) < double.Epsilon) return double.NaN;
            return constituents.Sum(T => T.Max()) / sum;
        }

    }
}
