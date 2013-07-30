using Ecosystem.Niche;
using MathUtils.Collections;

namespace Ecosystem.Habitat.TGrid
{
    public interface IGridNiche : INiche
    {
        TorusPoint Location { get; }
    }
}