using System;
using System.Linq; 
using DynamicModel.Model;
using SortNetwork.Results;
using SortNetwork.Sorters;

namespace SortingNetworkDm.Entities
{
    public interface ISorterResultPoolEntity : IEntity
    {
        ISorterResultRepo SorterResultRepo { get; }
    }

    public static class SorterResultPoolEntity
    {
        public const string TypeName = "TheSorterResultPoolEntity";

        public static ISorterResultPoolEntity Make
            (
                Guid guid,
                string name,
                string description,
                ISorterResultRepo sorterResultRepo
            )
        {
            return new SorterResultPoolEntityImpl(guid, name, description, sorterResultRepo);
        }

        public static ISorterPoolEntity ToSorterPoolEntity(this ISorterResultPoolEntity sorterResultPoolEntity)
        {
            return SorterPoolEntity.Make
                (
                    guid:sorterResultPoolEntity.Guid,
                    name: sorterResultPoolEntity.Name,
                    description: sorterResultPoolEntity.Description,
                    sorterRepo: sorterResultPoolEntity.SorterResultRepo.Select(T=>T.Sorter).ToSorterRepo()
                );
        }
    }

    class SorterResultPoolEntityImpl : EntityImpl, ISorterResultPoolEntity
    {
        public SorterResultPoolEntityImpl(Guid guid, string name, string description, 
                ISorterResultRepo sorterResultRepo)
            : base(guid, name, description)
        {
            _sorterResultRepo = sorterResultRepo;
        }

        public override bool IsLoaded
        {
            get { return SorterResultRepo != null; }
        }

        public override string TypeName { get { return SorterResultPoolEntity.TypeName; } }

        private readonly ISorterResultRepo _sorterResultRepo;
        public ISorterResultRepo SorterResultRepo
        {
            get { return _sorterResultRepo; }
        }
    }

}
