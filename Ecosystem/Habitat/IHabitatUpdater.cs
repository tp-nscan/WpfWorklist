using System;
using System.Collections.Generic;
using MathUtils.Repos;

namespace Ecosystem.Habitat
{
    public interface IHabitatUpdater
    {
        IHabitat Update
            (
                IHabitat oldHabitat,
                IReadOnlyCollection<int> randMigrate,
                IReadOnlyCollection<double> randMigrateDirection,
                IReadOnlyCollection<double> randMutateGenes,
                IReadOnlyCollection<Tuple<double, double>> randMutateRates
            );
    }
}