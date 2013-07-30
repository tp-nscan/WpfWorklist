using System;
using System.Collections.Generic;

namespace Ecosystem.Niche
{
    public interface INiche
    {
        Guid Guid { get; }
        IEnumerable<IOrganisim> Organisims { get; }
        IEnumerable<IOrganisim> Migrants { get; }
        int OrganisimCount { get; }
        int MigrantCount { get; }
    }
}
