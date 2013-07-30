using System;
using System.Collections.Generic;
using Ecosystem.Niche;
using MathUtils.Repos;

namespace Ecosystem.Migrator
{
    public class ImmigrantsForNiches : IImmigrantsForNiches
    {
        public ImmigrantsForNiches(IKeyedRepo<Guid, INiche> habitat)
        {
            foreach (var niche in habitat.Items)
            {
               _nicheImmigrantses.Add(niche.Guid, new NicheImmigrants(niche.Guid)); 
            }    
        }

        readonly Dictionary<Guid, NicheImmigrants> _nicheImmigrantses = new Dictionary<Guid, NicheImmigrants>();
        public INicheImmigrants NicheImmigrantsByNicheId(Guid guid)
        {
            return (_nicheImmigrantses.ContainsKey(guid)) ? _nicheImmigrantses[guid] : null;
        }
    }
}
