using System.Collections.Generic;
using System.Linq;
using SortNetwork.Json.Sorters;
using SortNetwork.Results;

namespace SortNetwork.Json.Results
{
    public class SorterResultToJson
    {
        public static SorterResultToJson ToJsonAdapter(ISorterResult sorterResult)
        {
            var sorterToJson = new SorterResultToJson
            {
                Sorter = SorterToJson.ToJsonAdapter(sorterResult.Sorter),
                SuccessfulSorts = sorterResult.SuccessfulSorts,
                SwitchResultsToJson = sorterResult.SwitchResults.Select(SwitchResultToJson.Make).ToList()
            };

            return sorterToJson;
        }

        public static ISorterResult ToSorterResult(SorterResultToJson sorterResultToJson)
        {
                return SorterResult.Make
                (
                    sorter: SorterToJson.ToSorter(sorterResultToJson.Sorter),
                    switchResults: sorterResultToJson.SwitchResultsToJson.Select(T => T.V.ToSwitchResult(sorterResultToJson.Sorter.KeyCount)),
                    countOfTests: sorterResultToJson.CountOfTests,
                    successfulSorts: sorterResultToJson.SuccessfulSorts,
                    switchesUsed: sorterResultToJson.SwitchesUsed
                );
        }

        public SorterToJson Sorter { get; set; }

        public int CountOfTests { get; set; }

        public List<SwitchResultToJson> SwitchResultsToJson { get; set; }

        public int SuccessfulSorts { get; set; }

        public int SwitchesUsed { get; set; }
    }
}
