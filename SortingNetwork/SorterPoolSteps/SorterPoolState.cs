using System;
using System.Collections.Generic;
using System.Linq;
using SortingNetwork.SorterPoolOpsSpec;
using SortingNetwork.Sorters;

namespace SortingNetwork.SorterPoolSteps
{
    public static class SorterPoolState
    {
        public static ISorterPoolState Make
            (
                Guid guid, 
                IEnumerable<ISorterPoolOp> sorterPoolOps, 
                IEnumerable<ISorter> sorters, 
                string comment
            )
        {
            return new SorterPoolStepImpl(guid, sorterPoolOps, sorters, comment);
        }
    }
    
    class SorterPoolStepImpl : ISorterPoolState
    {
        public SorterPoolStepImpl
            (
                Guid guid, 
                IEnumerable<ISorterPoolOp> sorterPoolOps, 
                IEnumerable<ISorter> sorters, 
                string comment
            )
        {
            SorterPool = sorters.ToSorterRepo();
            _guid = guid;
            _sorterPoolOps = sorterPoolOps.ToList();
            _comment = comment;
        }

        private readonly string _comment;
        public string Comment
        {
            get { return _comment; }
        }

        private readonly Guid _guid;
        public Guid Guid
        {
            get { return _guid; }
        }
        
        private readonly List<ISorterPoolOp> _sorterPoolOps;
        public IEnumerable<ISorterPoolOp> SorterPoolOps
        {
            get { return _sorterPoolOps; }
        }

        public ISorterRepo<ISorter> SorterPool { get; private set; }

    }
}
