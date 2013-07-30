using SortingNetwork.Switches;

namespace SortingNetwork.SorterMonitors
{
    public interface ISwitchMonitor : ISwitch
    {
        int UseCount { get; set; }
    }
}
