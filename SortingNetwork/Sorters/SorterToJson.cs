using System;
using System.Collections.Generic;
using System.Linq;
using SortingNetwork.Switches;

namespace SortingNetwork.Sorters
{
    public class SorterToJson
    {
        public static SorterToJson ToJsonAdapter(ISorter sorter)
        {
            var sorterToJson = new SorterToJson
            {
                Guid = sorter.Guid,
                KeyCount = sorter.KeyCount,
                SorterType = sorter.SorterType,
                Switches = sorter.Switches.Select(SwitchToJson.Make).ToList(),
            };

            return sorterToJson;
        }

        public Guid Guid { get; set; }

        public int KeyCount { get; set; }

        public List<SwitchToJson> Switches { get; set; }

        public SorterType SorterType { get; set; }

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
