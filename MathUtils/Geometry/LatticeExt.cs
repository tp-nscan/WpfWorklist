using System.Collections.Generic;

namespace MathUtils.Geometry
{
    public static class LatticeExt
    {
        public static IEnumerable<LatticeCell<T>> LookLeft<T>(this LatticeCell<T> latticeCell)
        {
            if(latticeCell.West != null)
            {
                yield return latticeCell.West;

                if(latticeCell.West.North != null)
                {
                    yield return latticeCell.West.North;
                }
                if (latticeCell.West.South != null)
                {
                    yield return latticeCell.West.South;
                }
            }
        }

        public static IEnumerable<LatticeCell<T>> LookRight<T>(this LatticeCell<T> latticeCell)
        {
            if (latticeCell.East != null)
            {
                yield return latticeCell.East;

                if (latticeCell.East.North != null)
                {
                    yield return latticeCell.East.North;
                }
                if (latticeCell.East.South != null)
                {
                    yield return latticeCell.East.South;
                }
            }
        }
    }
}
