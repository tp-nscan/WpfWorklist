using System.Collections.Generic;

namespace Genomic.Locus
{
    public class LinkedLocus : ILinkedLocus
    {
        private readonly ILocus _locus;
        private readonly ILinkedLocus _prev;
        private readonly ILinkedLocus _next;

        public LinkedLocus
            (
                ILocus data,
                ILinkedLocus prev,
                IEnumerator<ILocus> rest
            )
        {
            _locus = data;
            _prev = prev;
            if (rest.MoveNext())
            {
                _next = new LinkedLocus
                    (
                        rest.Current,
                        this, 
                        rest
                    );
            }
        }

        public static LinkedLocus Create(IEnumerable<ILocus> loci)
        {
            using (var enumerator = loci.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return null;
                }

                return new LinkedLocus
                    (
                        enumerator.Current,
                        null,
                        enumerator
                    );
            }
        }

        public ILocus Locus
        {
            get { return _locus; }
        }

        public IEnumerable<ILinkedLocus> Loci { 
            get
            {
                ILinkedLocus curStep = this;
                while (curStep != null)
                {
                    yield return curStep;
                    curStep = curStep.Next;
                }
            } 
        }

        public ILinkedLocus Prev
        {
            get { return _prev; }
        }

        public ILinkedLocus Next
        {
            get { return _next; }
        }
    }

}
