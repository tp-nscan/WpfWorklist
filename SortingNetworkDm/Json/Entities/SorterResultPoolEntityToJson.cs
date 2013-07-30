using System;
using SortNetwork.Json.Results;
using SortingNetworkDm.Entities;

namespace SortingNetworkDm.Json.Entities
{
    public class SorterResultPoolEntityToJson
    {
        public static SorterResultPoolEntityToJson ToJson(ISorterResultPoolEntity sorterResultPoolEntity)
        {
            return new SorterResultPoolEntityToJson
            {
                Description = sorterResultPoolEntity.Description,
                Guid = sorterResultPoolEntity.Guid,
                Name = sorterResultPoolEntity.Name,
                TypeName = sorterResultPoolEntity.TypeName,
                SorterResultRepoToJson = SorterResultRepoToJson
                    .ToJson(sorterResultPoolEntity.SorterResultRepo)
            };
        }

        public static ISorterResultPoolEntity ToSorterResultPoolEntity(SorterResultPoolEntityToJson sorterResultPoolEntityToJson)
        {
            return SorterResultPoolEntity.Make
                (
                    guid: sorterResultPoolEntityToJson.Guid,
                    name: sorterResultPoolEntityToJson.Name,
                    description: sorterResultPoolEntityToJson.Description,                    
                    sorterResultRepo: SorterResultRepoToJson.ToSorterResultRepo(sorterResultPoolEntityToJson.SorterResultRepoToJson)
                );
        }

        public string Description { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }

        public SorterResultRepoToJson SorterResultRepoToJson { get; set; }
    }
}
