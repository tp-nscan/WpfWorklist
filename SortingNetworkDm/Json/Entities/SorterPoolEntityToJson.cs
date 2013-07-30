using System;
using SortNetwork.Json.Sorters;
using SortingNetworkDm.Entities;

namespace SortingNetworkDm.Json.Entities
{
    public class SorterPoolEntityToJson
    {
        public static SorterPoolEntityToJson ToJson(ISorterPoolEntity sorterPoolEntity)
        {
            return new SorterPoolEntityToJson
            {
                Description = sorterPoolEntity.Description,
                Guid = sorterPoolEntity.Guid,
                Name = sorterPoolEntity.Name,
                TypeName = sorterPoolEntity.TypeName,
                SorterRepoToJson = SorterRepoToJson.ToJson(sorterPoolEntity.SorterRepo)
            };
        }

        public static ISorterPoolEntity ToSorterPoolEntity(SorterPoolEntityToJson sorterPoolEntityToJson)
        {
            return SorterPoolEntity.Make
                (
                    guid: sorterPoolEntityToJson.Guid,
                    name: sorterPoolEntityToJson.Name,
                    description: sorterPoolEntityToJson.Description,
                    sorterRepo: SorterRepoToJson.ToSorterRepo(sorterPoolEntityToJson.SorterRepoToJson)
                );
        }

        public string Description { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }

        public SorterRepoToJson SorterRepoToJson { get; set; }
    }

}
