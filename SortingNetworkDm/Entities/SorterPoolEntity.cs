using System;
using DynamicModel.Model;
using SortNetwork.Sorters;

namespace SortingNetworkDm.Entities
{
    public interface ISorterPoolEntity : IEntity
    {
        ISorterRepo SorterRepo { get; }
    }

    public static class SorterPoolEntity
    {
        public const string TypeName = "TheSorterPoolEntity";

        public static ISorterPoolEntity Make
            (
                Guid guid,
                string name,
                string description,
                ISorterRepo sorterRepo
            )
        {
            return new SorterPoolEntityImpl(guid, name, description, sorterRepo);
        }
    }

    class SorterPoolEntityImpl : EntityImpl, ISorterPoolEntity
    {
        public SorterPoolEntityImpl(Guid guid, string name, string description, ISorterRepo sorterRepo) 
            : base(guid, name, description)
        {
            _sorterRepo = sorterRepo;
        }

        public override bool IsLoaded
        {
            get { return SorterRepo != null; }
        }

        public override string TypeName { get { return SorterPoolEntity.TypeName; } }

        private readonly ISorterRepo _sorterRepo;
        public ISorterRepo SorterRepo
        {
            get { return _sorterRepo; }
        }
    }
}
