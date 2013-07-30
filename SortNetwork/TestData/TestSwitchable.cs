using MathUtils.Rand;
using SortNetwork.Switchables;

namespace SortNetwork.TestData
{
    public static class TestSwitchable
    {
        private static ISwitchableRepo _theSwitchableRepo;

        public static ISwitchableRepo TheSwitchableRepo 
        {
            get
            {
                return _theSwitchableRepo ??
                    (
                        _theSwitchableRepo = SwitchableBitArray.MakeRandoms
                            (
                               keyCount: TestConstants.KeyCount, 
                               random: Randy.Fast(TestConstants.Seed),
                               itemCount: TestConstants.SwitchableCount
                            ).ToSwitchableRepo()
                    );
            }
        }

        public static ISwitchableRepo SwitchableRepo(int seed)
        {
            return SwitchableBitArray.MakeRandoms
                (
                    keyCount: TestConstants.KeyCount,
                    random: Randy.Fast(seed),
                    itemCount: TestConstants.SwitchableCount
                ).ToSwitchableRepo();
        }
    }
}
