using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonLib
{
    public class ModuleGroup
    {
        public ModuleGroup(string header, IEnumerable<IModule> modules)
        {
            _header = header;
            _modules.Add(new HeaderModule(Header));
            _modules.AddRange(modules);
        }

        private readonly string _header;
        public string Header
        {
            get { return _header; }
        }

        private readonly List<IModule> _modules = new List<IModule>();
        public IEnumerable<IModule> Modules
        {
            get { return _modules; }
        }
    }
}
