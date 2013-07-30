using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SortingNetwork.SorterMonitors;

namespace SortingNetwork.Sorters
{
    public class SorterRepoToJson
    {
        public static SorterRepoToJson ToJsonAdapter(ISorterRepo<ISorter> sorterRepo)
        {
            var sorterList = new List<object>();

            foreach (var sorter in sorterRepo.Items)
            {
                if (sorter.SorterType == SorterType.Simple)
                {
                    sorterList.Add(SorterToJson.ToJsonAdapter(sorter));
                }
                else
                {
                    sorterList.Add(SorterMonitorToJson.ToJsonAdapter((ISorterMonitor)sorter));
                }
            }

            return new SorterRepoToJson()
            {
                Sorters = sorterList
            };
        }


        public static ISorterRepo<ISorter> ToSorterRepo(SorterRepoToJson sorterPoolToJson)
        {
            return sorterPoolToJson.Sorters.Select(ConvertToSorter).ToSorterRepo();
        }

        public static ISorter ConvertToSorter(object jsonObj)
        {
            var type = jsonObj.GetType();
            if (type == typeof(SorterToJson))
            {
                return SorterToJson.ToSorter((SorterToJson)jsonObj);
            }
            if (type == typeof(SorterMonitorToJson))
            {
                return SorterMonitorToJson.ToSorterMonitor((SorterMonitorToJson)jsonObj);
            }
            throw new ArgumentException("cant convert to SorterToJson");
        }

        [JsonConverter(typeof(SorterJsonConverter))]
        public List<object> Sorters { get; set; }
    }
}
