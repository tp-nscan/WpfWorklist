using System.Collections.Generic;
using System.Linq;
using SortNetwork.Results;

namespace SortNetwork.Json.Results
{
    public class SorterResultRepoToJson
    {
        public static SorterResultRepoToJson ToJson(IReadOnlyCollection<ISorterResult> sorterRepo)
        {
            var sorterList = sorterRepo
                .Select(SorterResultToJson.ToJsonAdapter).ToList();

            return new SorterResultRepoToJson()
            {
                SorterResultToJsons = sorterList
            };
        }


        public static ISorterResultRepo ToSorterResultRepo(SorterResultRepoToJson sorterPoolToJson)
        {
            return sorterPoolToJson.SorterResultToJsons
                        .Select(SorterResultToJson.ToSorterResult)
                        .ToSorterResultRepo();
        }

        public List<SorterResultToJson> SorterResultToJsons { get; set; }
    }
}
