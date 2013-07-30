using System;
using System.Collections.Immutable;
using Ecosystem.Migrator;

namespace Ecosystem.Niche
{
    public interface INicheUpdater
    {
        INiche UpdateNiche
            (
                INiche oldNiche, 
                INicheImmigrants nicheImmigrants,
                IImmutableStack<double> rndMigrateDirection,
                IImmutableStack<double> rndMutateGenes,
                IImmutableStack<Tuple<double, double>> rndMutateRates
            );
    }
}