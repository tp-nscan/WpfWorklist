using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MathUtils.Repos;

namespace SortNetwork.KeySets
{
    public static class KeyPairRepo
    {
        public static IReadOnlyCollection<IKeyPair> ToKeyPairRepo(this IEnumerable<IKeyPair> keyPairs)
        {
            return new KeyPairRepoImpl(keyPairs);
        }
    }

    class KeyPairRepoImpl : ReadOnlyCollection<IKeyPair> 
    {
        public KeyPairRepoImpl(IEnumerable<IKeyPair> keyPairs)
            : base(keyPairs.ToList())
        {

#if SAFE_MODE
            var keyCountGroups = Items.GroupBy(T => T.KeyCount).ToList();
            if (keyCountGroups.Count != 1)
            {
                throw new Exception("switchRepos must all have the same KeyCount");
            }
#endif
        }

        public int KeyCount
        {
            get { return this.Any() ? this[0].KeyCount : 0; ; }
        }
    }
}
