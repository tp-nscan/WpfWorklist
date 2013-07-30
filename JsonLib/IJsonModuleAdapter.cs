using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonLib
{
    public interface IJsonModuleAdapter
    {
        Guid Guid { get; }
        bool IsKnown { get; }
        string KeyForType { get; }
        IModule ToModule();
    }
}
