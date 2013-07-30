using System;
using System.Collections.Generic;
using System.Linq;
using SortingNetwork.Sorters;

namespace SortingNetwork.SorterMonitors
{
    public class SorterMonitorToJson
    {
        public static SorterMonitorToJson ToJsonAdapter(ISorterMonitor sorter)
        {
            var sorterToJson = new SorterMonitorToJson
            {
                Guid = sorter.Guid,
                KeyCount = sorter.KeyCount,
                SorterType = sorter.SorterType,
                Switches = sorter.Switches.Select(SwitchMonitorToJson.Make).ToList()
            };

            return sorterToJson;
        }

        public static ISorterMonitor ToSorterMonitor(SorterMonitorToJson sorterMonitorToJson)
        {
            return sorterMonitorToJson.Switches.Select
            (
                T => T.V.ToSwitchMonitor(sorterMonitorToJson.KeyCount)
            )
                .ToSorterMonitor(sorterMonitorToJson.Guid);
        }


        public Guid Guid { get; set; }

        public int KeyCount { get; set; }

        public List<SwitchMonitorToJson> Switches { get; set; }

        public SorterType SorterType { get; set; }
    }
}
