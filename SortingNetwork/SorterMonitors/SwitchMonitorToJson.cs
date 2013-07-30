using SortingNetwork.KeyPair;

namespace SortingNetwork.SorterMonitors
{
    public class SwitchMonitorToJson
    {
        public static SwitchMonitorToJson Make(ISwitchMonitor @switch)
        {
            return new SwitchMonitorToJson
            {
                V = @switch.ToShortString()
            };
        }

        public string V { get; set; }

    }

    public static class SwitchMonitorToJsonEx
    {
        public static ISwitchMonitor ToSwitchMonitor(this string shortString, int keyCount)
        {
            var pcs = shortString.Split(" ".ToCharArray());
            return SwitchMonitor.Make
                (
                    int.Parse(pcs[0]),
                    KeySet.Instance.GetKeyPair(int.Parse(pcs[1]), int.Parse(pcs[2]), keyCount),
                    int.Parse(pcs[3])
                );
        }

        public static string ToShortString(this ISwitchMonitor @switch)
        {
            return string.Format
                (
                    "{0} {1} {2} {3}", 
                    @switch.Index, 
                    @switch.KeyPair.LowKey,
                    @switch.KeyPair.HiKey,
                    @switch.UseCount
                );
        }
    }
}
