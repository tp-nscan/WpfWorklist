using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SortingNetwork.SorterPoolOpsSpec;
using SortingNetwork.Sorters;

namespace SortingNetwork.SorterPoolSteps
{
    public class SorterPoolStateToJson
    {
        public static SorterPoolStateToJson Make(ISorterPoolState sorterPoolState)
        {
            return new SorterPoolStateToJson
                {
                     Guid = sorterPoolState.Guid,
                     SorterPoolOps = sorterPoolState.SorterPoolOps.ToList(),
                     SorterRepoToJson = SorterRepoToJson.ToJsonAdapter(sorterPoolState.SorterPool)
                };
        }

        public static ISorterPoolState ToSorterPool(SorterPoolStateToJson sorterPoolStateToJson)
        {
            return SorterPoolState.Make
                (
                    sorterPoolStateToJson.Guid,
                    sorterPoolStateToJson.SorterPoolOps,
                    SorterRepoToJson.ToSorterRepo(sorterPoolStateToJson.SorterRepoToJson).Items,
                    sorterPoolStateToJson.Comment
                );
        }

        private SorterPoolStateToJson()
        {
        }

        public string Comment { get; set; }

        public Guid Guid { get; set; }

        public SorterRepoToJson SorterRepoToJson { get; set; }

        [JsonConverter(typeof(SorterPoolOpJsonConverter))]
        public List<ISorterPoolOp> SorterPoolOps { get; set; }
    }
}
