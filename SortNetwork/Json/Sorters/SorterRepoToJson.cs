using System.Collections.Generic;
using System.Linq;
using SortNetwork.Sorters;

namespace SortNetwork.Json.Sorters
{
    public class SorterRepoToJson
    {
        public static SorterRepoToJson ToJson(ISorterRepo sorterRepo)
        {
            var sorterList = sorterRepo
                .Select(SorterToJson.ToJsonAdapter).ToList();

            return new SorterRepoToJson
            {
                SorterToJsons = sorterList
            };
        }


        public static ISorterRepo ToSorterRepo(SorterRepoToJson sorterPoolToJson)
        {
            return sorterPoolToJson.SorterToJsons.Select(SorterToJson.ToSorter).ToSorterRepo();
        }

        public List<SorterToJson> SorterToJsons { get; set; }
    }
}
