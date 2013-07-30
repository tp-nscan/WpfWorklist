using System;

namespace SortingNetwork.SorterPoolOpsSpec
{
    public class SorterPoolOpManual : ISorterPoolOp
    {
        private static readonly Lazy<SorterPoolOpManual> lazy = new Lazy<SorterPoolOpManual>(() => new SorterPoolOpManual());

        public static SorterPoolOpManual Instance { get { return lazy.Value; } }

        private SorterPoolOpManual() { }

        public SorterPoolOpType SorterPoolOpType { get { return SorterPoolOpType.Manual;} }

        public string Comment { get { return string.Empty; } }
    }
}
