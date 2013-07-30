using System;

namespace Genomic.Locus
{
    public interface ILocus
    {
        Guid Guid { get; }
        LocusType LocusType { get; }
        ILocus Replicate(double rando);
    }
}