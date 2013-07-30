using System;
using System.Linq;
using System.Collections.Generic;
using Ecosystem.Niche;
using MathUtils.Repos;

namespace Ecosystem.Migrator
{
    public class HabitatMigrator
    {
        public static IImmigrantsForNiches Migrate(IKeyedRepo<Guid, INiche> habitatNiches, IHabitatMigrationRules habitatMigrationRules,
            IReadOnlyCollection<int> rndMigrate)
        {
            if (rndMigrate.Count != habitatNiches.Size)
            {
                throw new Exception("randMigrate count is not correct");
            }

            var immigrantsForNiches = new ImmigrantsForNiches(habitatNiches);

            var index = 0;
            foreach (var niche in habitatNiches.Items)
            {
                var nicheMigrationRule = habitatMigrationRules.NicheMigrationRule(niche);
                foreach (var tuple in nicheMigrationRule.DisperseMigrants(rndMigrate.ElementAt(index++), habitatNiches))
                {
                    immigrantsForNiches.NicheImmigrantsByNicheId(tuple.Item1).AddImmigrants(tuple.Item2);
                }
            }
            return immigrantsForNiches;
        }
    }
}
