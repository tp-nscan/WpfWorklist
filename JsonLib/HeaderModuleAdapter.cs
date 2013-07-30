using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonLib
{
    public class HeaderModuleAdapter : IJsonModuleAdapter
    {
        public static HeaderModuleAdapter Make(HeaderModule headerModule)
        {
            return new HeaderModuleAdapter
                {
                    KeyForType = headerModule.ModuleType,
                    Header = headerModule.Header,
                    IsKnown = true,
                    Guid = Guid.Empty
                };
        }

        public Guid Guid { get; set; }
        public string Header { get; set; }
        public bool IsKnown { get; set; }
        public string KeyForType { get; set; }

        public IModule ToModule()
        {
            return new HeaderModule(Header);
        }
    }
}
