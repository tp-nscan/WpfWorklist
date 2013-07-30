using System.Collections.Generic;

namespace MathUtils.Repos
{
    public interface IKeyedRepo<in TK, out TV>
    {
        TV GetValue(TK key);
        TV this[TK key] { get; }
        IEnumerable<TV> Items { get; }
        int Size { get; }
    }
}