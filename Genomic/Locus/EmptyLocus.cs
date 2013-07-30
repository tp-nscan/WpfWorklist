using System;

namespace Genomic.Locus
{
    public class EmptyLocus : LocusBase
    {
        public EmptyLocus(Guid guid) : base
            (
               guid: guid, 
               replicator: (l, d)=>new EmptyLocus(l.Guid)
            )
        {
        }

        public override LocusType LocusType
        {
            get { return LocusType.Empty;}
        }
    }
}
