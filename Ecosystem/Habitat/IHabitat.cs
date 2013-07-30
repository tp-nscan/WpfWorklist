using System;
using System.Collections.Generic;
using Ecosystem.Migrator;
using Ecosystem.Niche;

namespace Ecosystem.Habitat
{
    public interface IHabitat : IReadOnlyDictionary<Guid, INiche>
    {
        IHabitatMigrationRules HabitatMigrationRules { get; }
    }
}
