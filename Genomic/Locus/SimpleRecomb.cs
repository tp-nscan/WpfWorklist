using System;

namespace Genomic.Locus
{
    public class SimpleRecomb
    {
        public SimpleRecomb(Guid guid, double frequency)
        {
            _frequency = frequency;
            _guid = guid;
        }

        private readonly double _frequency;
        public double Frequency { get { return _frequency; } }

        private readonly Guid _guid;
        public Guid Guid
        {
            get { return _guid; }
        }
    }
}
