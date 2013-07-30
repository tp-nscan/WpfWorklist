using System.Collections.Generic;

namespace Lab.Model.Containers.Plate
{
    public class WellSorterReadingOrder : IComparer<IWellLoc>
    {
        public int Compare(IWellLoc x, IWellLoc y)
        {
            if (y.Row < x.Row)
            {
                return 1;
            }
            if (y.Row > x.Row)
            {
                return -1;
            }
            if (y.Column < x.Column)
            {
                return 1;
            }
            if (y.Column > x.Column)
            {
                return -1;
            }
            return 0;
        }
    }
}
