using System;
using System.Collections.Generic;
using SortingNetwork.SorterPoolOpsSpec;
using SortingNetwork.Sorters;

namespace SortingNetwork.SorterPoolSteps
{
    public interface ISorterPoolState
    {
        string Comment { get; }
        Guid Guid { get; }
        IEnumerable<ISorterPoolOp> SorterPoolOps { get; }
        ISorterRepo<ISorter> SorterPool { get; }
    }
}