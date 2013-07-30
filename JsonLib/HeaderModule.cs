using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonLib
{
    public class HeaderModule : IModule
    {
        private readonly string _header;

        public HeaderModule(string header)
        {
            _header = header;
        }

        public string Header
        {
            get { return _header; }
        }

        public string ModuleType { get { return JsonModuleArrayConverter.HeaderModuleType; } }
    }
}
