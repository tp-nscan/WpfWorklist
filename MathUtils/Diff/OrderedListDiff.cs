using System;
using System.Collections.Generic;

namespace MathUtils.Diff
{
    public class OrderedListDiff<T,U>
    {
        public OrderedListDiff(IEnumerable<T> setT, IEnumerable<U> setU, Func<T, U, bool> comp)
        {
            var enumT = setT.GetEnumerator();
            var enumU = setU.GetEnumerator();

            while (enumT.MoveNext() && enumU.MoveNext())
            {
                if (! comp(enumT.Current, enumU.Current))
                {
                    _diffs.Add(new Tuple<T, U>(enumT.Current, enumU.Current));
                }
            }
        }

        private readonly List<Tuple<T, U>> _diffs = new List<Tuple<T, U>>();
        public IEnumerable<Tuple<T,U>> Diffs
        {
            get { return _diffs; }
        }
    }
}
