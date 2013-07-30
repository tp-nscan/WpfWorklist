using System;
using System.Collections.Generic;

namespace SortingNetwork.Switchables
{
    public interface ISwitchablePool
    {
        IEnumerable<ISwitchable> Switchables { get; }
        Guid Guid { get; }
        string Comment { get; }
    }
}