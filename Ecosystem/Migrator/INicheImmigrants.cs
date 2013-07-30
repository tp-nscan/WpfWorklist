using System;
using System.Collections.Generic;

namespace Ecosystem.Migrator
{
    public interface INicheImmigrants
    {
        void AddImmigrants(IEnumerable<IOrganisim> immigrants);
        Guid Guid { get; }
        IEnumerable<IOrganisim> Immigrants { get; }
    }
}
