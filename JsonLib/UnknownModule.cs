using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonLib
{
    public class UnknownModule : IModule
    {
        public UnknownModule(string unknownKeyForType)
        {
            _unknownKeyForType = unknownKeyForType;
        }

        public string ModuleType { get { return JsonModuleArrayConverter.UnknownModuleType; } }

        private readonly string _unknownKeyForType;
        public string UnknownKeyForType
        {
            get { return _unknownKeyForType; }
        }
    }
}
