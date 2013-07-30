using System;
using System.Collections.Generic;
using System.Linq;
using Ecosystem.Migrator;
using Ecosystem.Niche;

namespace Ecosystem.Habitat.TGrid
{
    public class GridMigrationRules : IHabitatMigrationRules
    {
        public GridMigrationRules(IGridHabitat gridHabitat)
        {
            _gridHabitat = gridHabitat;

            foreach (var gridNich in GridHabitat.Niches)
            {
                _nicheMigrationRules.Add
                    (
                        gridNich.Guid, 
                        new UniformNicheMigrationRule(gridNich.Guid, gridNich.Location.EightNeighbors.Select(T => GridHabitat.NicheAtLocation(T).Guid))
                    );
            }
        }

        private readonly IGridHabitat _gridHabitat;
        public IGridHabitat GridHabitat
        {
            get { return _gridHabitat; }
        }


        readonly Dictionary<Guid, INicheMigrationRule> _nicheMigrationRules = new Dictionary<Guid, INicheMigrationRule>();
        public INicheMigrationRule NicheMigrationRule(INiche niche)
        {
            return _nicheMigrationRules.ContainsKey(niche.Guid) ? _nicheMigrationRules[niche.Guid] : null;
        }
    }
}
