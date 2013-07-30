using System;

namespace Genomic.Locus
{
    public abstract class LocusBase : ILocus
    {
        protected LocusBase(Guid guid, Func<ILocus, double, ILocus> replicator)
        {
            _guid = guid;
            _replicator = replicator;
        }

        private readonly Guid _guid;
        public Guid Guid
        {
            get { return _guid; }
        }

        public abstract LocusType LocusType { get; }

        private readonly Func<ILocus, double, ILocus> _replicator;
        public ILocus Replicate(double rando)
        {
            return _replicator(this, rando);
        }

    }
}
