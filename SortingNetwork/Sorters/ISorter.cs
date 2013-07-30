using System;
using System.Collections.Generic;
using SortingNetwork.Switchables;
using SortingNetwork.Switches;

namespace SortingNetwork.Sorters
{
    public interface ISorter
    {
        Guid Guid { get; }
        int KeyCount { get; }
        ISwitchable Sort(ISwitchable switchable);
        SorterType SorterType { get; }
        IEnumerable<ISwitch> Switches { get; }
    }

    public interface ISorter<T> : ISorter where T : ISwitch
    {
        new IEnumerable<T> Switches { get; }
    }
}
