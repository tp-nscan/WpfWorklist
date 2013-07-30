using System;
using System.Collections.Generic;
using System.Linq;
using Ecosystem.Niche;
using MathUtils.Collections;
using MathUtils.Repos;

namespace Ecosystem.Migrator
{
    public class UniformNicheMigrationRule : INicheMigrationRule
    {
        public UniformNicheMigrationRule(Guid sourceNicheId, IEnumerable<Guid> targetNicheIds)
        {
            _sourceNicheId = sourceNicheId;
            _targetNicheIds = targetNicheIds.ToList();
        }

        private readonly List<Guid> _targetNicheIds;
        IEnumerable<Guid> TargetNicheIds
        {
            get { return _targetNicheIds; }
        }

        private readonly Guid _sourceNicheId;
        public Guid SourceNicheId
        {
            get { return _sourceNicheId; }
        }

        public IEnumerable<Tuple<Guid, IList<IOrganisim>>> DisperseMigrants(int randomizer, IKeyedRepo<Guid, INiche> repo)
        {
            var sourceNiche = repo.GetValue(SourceNicheId);
            var sprinkler = TargetNicheIds.Select(repo.GetValue).RoundRobin(randomizer).GetEnumerator();
            var dispersal = TargetNicheIds.Select(T => new Tuple<Guid, IList<IOrganisim>>(T, new List<IOrganisim>())).ToList();

            foreach (var migrant in sourceNiche.Migrants)
            {
                sprinkler.MoveNext();
                dispersal.Single(T => T.Item1 == sprinkler.Current.Guid).Item2.Add(migrant);
            }
            return dispersal;
        }
    }
}
