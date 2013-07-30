using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ecosystem;
using Ecosystem.Habitat;
using Ecosystem.Habitat.TGrid;
using Ecosystem.Migrator;
using Ecosystem.Niche;
using MathUtils.Rand;
using MathUtils.Repos;

namespace GaSimulations.DblVal
{
    public class DblVectSim
    {
        public static IGridHabitat MakeHabitat(int gridSize, int vectorLength, int orgsPerNiche, int migrantsPerNiche,
                                               int replcantsPerOrg, double mutationRate, int seed)
        {
            var randomInt = Randy.Fast(seed).ToInt();

            var orgVectors = Enumerable.Range(0, gridSize*gridSize*orgsPerNiche).Select
                (
                    i => new ReadOnlyCollection<double>
                        (
                            Generators.Doubles(vectorLength, randomInt.Next()).ToList()
                        )
                );

            var migrantVectors = Enumerable.Range(0, gridSize * gridSize * migrantsPerNiche).Select
                (
                    i => new ReadOnlyCollection<double>
                        (
                            Generators.Doubles(vectorLength, randomInt.Next()).ToList()
                        )
                );

            var orgs = orgVectors.Select(T =>
                {
                    var immutableStack = T.MakeStack();
                    return new DblVectOrg(Guid.NewGuid(), Guid.NewGuid(), ref immutableStack, mutationRate, replcantsPerOrg);
                });

            var migrants = migrantVectors.Select(T =>
                {
                    var makeStack = T.MakeStack();
                    return new DblVectOrg(Guid.NewGuid(), Guid.NewGuid(), ref makeStack, mutationRate, replcantsPerOrg);
                });

            return new GridHabitat
                (
                    gridSize,
                    //new ReadOnlyCollection<IOrganisim>(orgs.ToList()),
                    //new ReadOnlyCollection<IOrganisim>(migrants),
                    new ReadOnlyCollection<IOrganisim>(Enumerable.Empty<IOrganisim>().ToList()),
                    new ReadOnlyCollection<IOrganisim>(Enumerable.Empty<IOrganisim>().ToList()),
                    orgsPerNiche,
                    migrantsPerNiche,
                    gh => new GridMigrationRules(gh)
                );
        }

        public static IGridHabitat MakeHabitat(IHabitat oldHabitat, IReadOnlyCollection<INiche> niches, IHabitatMigrationRules habitatMigrationRules)
        {
            var gridHabitatOld = oldHabitat as IGridHabitat;
            if (gridHabitatOld == null)
            {
                throw new Exception("GridHabitat was null");
            }

            return new GridHabitat
                (
                    gridHabitatOld.GridSize,
                    niches.Cast<IGridNiche>(),
                    habitatMigrationRules
                );
        }

    }
}
