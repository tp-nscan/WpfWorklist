using System;
using System.Collections.Generic;

namespace Genomic.Locus
{
    public interface ILinkedLocus
    {
        ILocus Locus { get; }
        IEnumerable<ILinkedLocus> Loci { get; }
        ILinkedLocus Prev { get; }
        ILinkedLocus Next { get; }
    }
}
