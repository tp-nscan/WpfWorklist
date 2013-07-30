using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonLib
{
    public class UnknownModuleAdapter : IJsonModuleAdapter
    {
        public UnknownModuleAdapter(string keyForType)
        {
            KeyForType = keyForType;
        }
        public Guid Guid { get { return Guid.Empty; } }
        public string KeyForType { get; set; }
        public IModule ToModule()
        {
            return new UnknownModule(KeyForType);
        }

        public bool IsKnown { get { return false; } } 
    }
}
