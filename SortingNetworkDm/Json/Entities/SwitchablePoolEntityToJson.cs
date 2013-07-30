using System;
using SortNetwork.Json.Switchables;
using SortingNetworkDm.Entities;

namespace SortingNetworkDm.Json.Entities
{
    public class SwitchablePoolEntityToJson
    {
        public static SwitchablePoolEntityToJson ToJson(ISwitchablePoolEntity switchablePoolEntity)
        {
            return new SwitchablePoolEntityToJson
            {
                Name = switchablePoolEntity.Name,
                Description = switchablePoolEntity.Description,
                Guid = switchablePoolEntity.Guid,
                TypeName = switchablePoolEntity.TypeName,
                SwitchableRepoToJson = SwitchableRepoToJson.ToJson(switchablePoolEntity.SwitchableRepo)
            };
        }

        public static ISwitchablePoolEntity ToSwitchablePoolEntity(SwitchablePoolEntityToJson switchablePoolEntityToJson)
        {
            return SwitchablePoolEntity.Make
                (
                    guid: switchablePoolEntityToJson.Guid,
                    name: switchablePoolEntityToJson.Name,
                    description: switchablePoolEntityToJson.Description,
                    switchableRepo: SwitchableRepoToJson.ToSwitchableRepo(switchablePoolEntityToJson.SwitchableRepoToJson)
                );
        }

        public string Description { get; set; }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string TypeName { get; set; }

        public SwitchableRepoToJson SwitchableRepoToJson { get; set; }
    }
}
