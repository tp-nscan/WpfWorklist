using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ecosystem.Migrator;
using Ecosystem.Niche;
using MathUtils.Repos;

namespace Ecosystem.Habitat
{
    public class HabitatUpdater : IHabitatUpdater
    {
        public HabitatUpdater(
            Func<IReadOnlyDictionary<Guid, INiche>, IHabitatMigrationRules, IReadOnlyCollection<int>, IImmigrantsForNiches> migrator,
            Func<IHabitat, IReadOnlyCollection<INiche>, IHabitatMigrationRules, IHabitat> habitatBuilder,
            INicheUpdater nicheUpdater)
        {
            _habitatBuilder = habitatBuilder;
            _nicheUpdater = nicheUpdater;
            _migrator = migrator;
        }

        private readonly Func<IReadOnlyDictionary<Guid, INiche>, IHabitatMigrationRules, IReadOnlyCollection<int>, IImmigrantsForNiches> _migrator;
        private readonly INicheUpdater _nicheUpdater;
        INicheUpdater NicheUpdater
        {
            get { return _nicheUpdater; }
        }

        private readonly Func<IHabitat, IReadOnlyCollection<INiche>, IHabitatMigrationRules, IHabitat> _habitatBuilder;
        public Func<IHabitat, IReadOnlyCollection<INiche>, IHabitatMigrationRules, IHabitat> HabitatBuilder
        {
            get { return _habitatBuilder; }
        }

        public IHabitat Update
            (
                IHabitat oldHabitat,
                IReadOnlyCollection<int> randMigrate,
                IReadOnlyCollection<double> randMigrateDirection,
                IReadOnlyCollection<double> randMutateGenes,
                IReadOnlyCollection<Tuple<double, double>> randMutateRate
            )
        {
            var habitatImmigrants = _migrator(oldHabitat, oldHabitat.HabitatMigrationRules, randMigrate);

            var newNiches = new ReadOnlyCollection<INiche>
                (
                    UpdatedNiches
                    (
                        oldNiches: new ReadOnlyCollection<INiche>(oldHabitat.Values.ToList()),
                        immigrants: habitatImmigrants,
                        rndMigrateDirection: randMigrateDirection,
                        rndMutateGenes: randMutateGenes,
                        randMutateRates: randMutateRate
                    ).ToList()
                );

            return HabitatBuilder(oldHabitat, newNiches, oldHabitat.HabitatMigrationRules);
        }

        IEnumerable<INiche> UpdatedNiches
                                (
                                    IReadOnlyCollection<INiche> oldNiches, 
                                    IImmigrantsForNiches immigrants,
                                    IReadOnlyCollection<double> rndMigrateDirection,
                                    IReadOnlyCollection<double> rndMutateGenes,
                                    IReadOnlyCollection<Tuple<double, double>> randMutateRates
                                )
        {
            var mutateGeneRandos = rndMutateGenes.Split(oldNiches.Count).ToList();
            var mutateRandoRates = randMutateRates.Split(oldNiches.Count).ToList();
            var migrateSelecteRandos = rndMigrateDirection.Split(oldNiches.Count).ToList();

            for (var nitcheDex = 0; nitcheDex < oldNiches.Count; nitcheDex++)
            {
                var currentNiche = oldNiches.ElementAt(nitcheDex);
                yield return
                    NicheUpdater.UpdateNiche
                    (
                        oldNiche: currentNiche,
                        nicheImmigrants: immigrants.NicheImmigrantsByNicheId(currentNiche.Guid),
                        rndMigrateDirection: migrateSelecteRandos[nitcheDex].MakeStack(),
                        rndMutateGenes: mutateGeneRandos[nitcheDex].MakeStack(),
                        rndMutateRates: mutateRandoRates[nitcheDex].MakeStack()
                    );
            }
        }

    }
}
