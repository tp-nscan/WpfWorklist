using System;
using System.Collections.Generic;
using System.Linq;
using SortNetwork.Sorters;

namespace SortNetwork.Json.Sorters
{
    public class SorterToJson
    {
        public static SorterToJson ToJsonAdapter(ISorter sorter)
        {
            var sorterToJson = new SorterToJson
            {
                Guid = sorter.Guid,
                KeyCount = sorter.KeyCount,
                //SuccessfulSorts = sorter.SuccessfulSorts,
                Switches = sorter.Switches.Select(SwitchToJson.Make).ToList(),
            };

            return sorterToJson;
        }

        public Guid Guid { get; set; }

        public int KeyCount { get; set; }

        public List<SwitchToJson> Switches { get; set; }

        //public SuccessfulSorts SuccessfulSorts { get; set; }

        public static ISorter ToSorter(SorterToJson sorterToJson)
        {
            return sorterToJson.Switches.Select
                (
                    T => T.V.ToSwitch(sorterToJson.KeyCount)
                )
                    .ToSorter(sorterToJson.Guid);
        }
    }
}
