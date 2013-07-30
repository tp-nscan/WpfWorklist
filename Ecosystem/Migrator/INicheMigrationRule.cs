using System;
using System.Collections.Generic;
using Ecosystem.Niche;
using MathUtils.Repos;

namespace Ecosystem.Migrator
{
    public interface INicheMigrationRule
    {
        Guid SourceNicheId { get; }
        IEnumerable<Tuple<Guid, IList<IOrganisim>>> DisperseMigrants(int randomizer, IKeyedRepo<Guid, INiche> repo);
    }
}
