using System.Collections.Generic;
using Ecosystem.Niche;
using MathUtils.Collections;

namespace Ecosystem.Habitat.TGrid
{
    public interface IGridHabitat : IHabitat
    {
        int GridSize { get; }
        INiche NicheAtLocation(TorusPoint location);
        IEnumerable<IGridNiche> Niches { get; } 
    }
}