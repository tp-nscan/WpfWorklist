using System;
using Ecosystem.Niche;

namespace Ecosystem
{
    public interface IEvaluator
    {
        Guid Guid { get; }
        INiche Evaluate(INiche niche);
    }
}
