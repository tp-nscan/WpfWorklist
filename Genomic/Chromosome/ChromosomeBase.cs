using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Genomic.Locus;
using MathUtils.Collections;

namespace Genomic.Chromosome
{
    public class ChromosomeBase : IChromosome
    {
        public ChromosomeBase
            (
                IEnumerable<ILocus> loci,
                Func<IChromosome, IImmutableStack<double>, IChromosome> replicator
            )
        {
            _root = LinkedLocus.Create(loci);
            _replicator = replicator;

            if (_root != null)
            {
                _centromere = _root.Loci.SingleOrDefault(T => T.Locus.LocusType == LocusType.Centromere);
            }
        }

        private readonly ILinkedLocus _root;

        private readonly ILinkedLocus _centromere;
        public ILinkedLocus Centromere
        {
            get { return _centromere; }
        }

        public IEnumerable<ILinkedLocus> Loci
        {
            get { return _root.Loci; }
        }

        private int? _locusLength;
        public int LocusLength
        {
            get
            {
                if (!_locusLength.HasValue)
                {
                    _locusLength = _root.Loci.Count();
                }
                return _locusLength.Value;
            }
        }

        private readonly Func<IChromosome, IImmutableStack<double>, IChromosome> _replicator;
        public IChromosome Replicate(ref IImmutableStack<double> randos)
        {
            IImmutableStack<double> subStack;
            randos = randos.MakeSubStack(out subStack, LocusLength);
            return _replicator(this, subStack);
        }
    }
}
