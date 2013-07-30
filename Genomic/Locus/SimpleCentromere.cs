using System;
using MathUtils.Collections;

namespace Genomic.Locus
{
    public class SimpleCentromere : IGuid
    {
        public SimpleCentromere(Guid guid, int index)
        {
            _index = index;
            _guid = guid;
        }

        private readonly int _index;
        public int Index { get { return _index; } }

        private readonly Guid _guid;
        public Guid Guid
        {
            get { return _guid; }
        }
    }
}
