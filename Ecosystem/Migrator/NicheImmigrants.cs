using System;
using System.Collections.Generic;

namespace Ecosystem.Migrator
{
    public class NicheImmigrants : INicheImmigrants
    {
        public NicheImmigrants(Guid guid)
        {
            _guid = guid;
        }

        public void AddImmigrants(IEnumerable<IOrganisim> immigrants)
        {
            _immigrants.AddRange(immigrants);
        }

        private readonly Guid _guid;
        public Guid Guid
        {
            get { return _guid; }
        }

        readonly List<IOrganisim> _immigrants = new List<IOrganisim>();

        public IEnumerable<IOrganisim> Immigrants { get { return _immigrants; } }
    }
}
