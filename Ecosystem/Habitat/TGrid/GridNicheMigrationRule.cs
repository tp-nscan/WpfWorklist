using System;
using System.Collections.Generic;
using System.Linq;
using Ecosystem.Migrator;
using Ecosystem.Niche;
using MathUtils.Collections;

namespace Ecosystem.Habitat.TGrid
{
    //public class GridNicheMigrationRule : INicheMigrationRule
    //{
    //    public GridNicheMigrationRule(IGridNiche sourceGridNiche, IEnumerable<IGridNiche> targetNiches)
    //    {
    //        _sourceGridNiche = sourceGridNiche;
    //        _targetNiches = targetNiches.ToList();
    //    }

    //    private readonly IGridNiche _sourceGridNiche;
    //    public IGridNiche SourceGridNiche
    //    {
    //        get { return _sourceGridNiche; }
    //    }

    //    private readonly List<IGridNiche> _targetNiches;
    //    IEnumerable<IGridNiche> TargetNiches
    //    {
    //        get { return _targetNiches; }
    //    }

    //    INiche INicheMigrationRule.SourceNicheId { get { return SourceGridNiche; } }

    //    public IEnumerable<Tuple<INiche, IList<IOrganisim>>> DisperseMigrants(int randomizer)
    //    {
    //        var sprinkler = SourceGridNiche.Location.EightNeighbors.RoundRobin(randomizer).GetEnumerator();
    //        var dispersal = TargetNiches.Select(T => new Tuple<IGridNiche, IList<IOrganisim>>(T, new List<IOrganisim>())).ToList();
    //        foreach (var migrant in SourceGridNiche.Migrants)
    //        {
    //            sprinkler.MoveNext();
    //            dispersal.Single(T => T.Item1.Location.Equals(sprinkler.Current)).Item2.Add(migrant);
    //        }
    //        return dispersal.Cast<Tuple<INiche, IList<IOrganisim>>>();
    //    }
    //}
}
