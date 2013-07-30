using System.Collections.Generic;
using MathUtils.Repos;

namespace SortingNetwork.KeyPair
{
    public static class KeyPairRepo
    {
        public static IKeyPairRepo ToKeyPairRepo(this IEnumerable<IKeyPair> keyPairs)
        {
            return new KeyPairRepoImpl(keyPairs);
        }
    }

    class KeyPairRepoImpl : Repo<IKeyPair>, IKeyPairRepo
    {
        public KeyPairRepoImpl(IEnumerable<IKeyPair> keyPairs)
            : base(keyPairs)
        {
            if (Size == 0) { return; }

#if SAFE_MODE
            var keyCountGroups = Items.GroupBy(T => T.KeyCount).ToList();
            if (keyCountGroups.Count != 1)
            {
                throw new Exception("switchRepos must all have the same KeyCount");
            }
#endif
            _keyCount = this[0].KeyCount;
        }

        private readonly int _keyCount;
        public int KeyCount
        {
            get { return _keyCount; }
        }
    }
}
