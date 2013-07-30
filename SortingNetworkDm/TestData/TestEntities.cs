using System;
using SortNetwork.TestData;
using SortingNetworkDm.Entities;

namespace SortingNetworkDm.TestData
{
    public static class TestEntities
    {

        #region SorterPoolEntity

        private static ISorterPoolEntity _theSorterPoolEntity;
        public static ISorterPoolEntity TheSorterPoolEntity
        {
            get
            {
                return _theSorterPoolEntity ??
                    (
                        _theSorterPoolEntity =

                            SorterPoolEntity.Make
                            (
                                guid: TestConstantsDm.SorterPoolEntityGuid,
                                name: TestConstantsDm.SorterPoolEntityName,
                                description: TestConstantsDm.SorterPoolEntityDescription,
                                sorterRepo: TestSorters.TheSorterRepo
                            )
                    );
            }
        }


        public static ISorterPoolEntity TestSorterPoolEntity(int repoSeed)
        {
            return
                    SorterPoolEntity.Make
                    (
                        guid: Guid.NewGuid(),
                        name: TestConstantsDm.SorterPoolEntityName,
                        description: TestConstantsDm.SorterPoolEntityDescription,
                        sorterRepo: TestSorters.SorterRepo(repoSeed)
                    );
        }

        #endregion


        #region SorterResultPoolEntity


        private static ISorterResultPoolEntity _theSorterResultPoolEntity;
        public static ISorterResultPoolEntity TheSorterResultPoolEntity
        {
            get
            {
                return _theSorterResultPoolEntity ??
                    (
                        _theSorterResultPoolEntity =

                            SorterResultPoolEntity.Make
                            (
                                guid: TestConstantsDm.SorterResultPoolEntityGuid,
                                name: TestConstantsDm.SorterResultPoolEntityName,
                                description: TestConstantsDm.SorterResultPoolEntityDescription,
                                sorterResultRepo: TestSorterResults.TheSorterResultRepo
                            )
                    );
            }
        }

        public static ISorterResultPoolEntity TestSorterResultPoolEntity(int repoSeed)
        {
            return
                    SorterResultPoolEntity.Make
                    (
                        guid: Guid.NewGuid(),
                        name: TestConstantsDm.SorterResultPoolEntityName,
                        description: TestConstantsDm.SorterResultPoolEntityDescription,
                        sorterResultRepo: TestSorterResults.SorterResultRepo(repoSeed)
                    );
        }

        #endregion


        #region SwitchablePoolEntity

        private static ISwitchablePoolEntity _theSwitchablePoolEntity;
        public static ISwitchablePoolEntity TheSwitchablePoolEntity
        {
            get
            {
                return _theSwitchablePoolEntity ??
                    (
                        _theSwitchablePoolEntity =
                            SwitchablePoolEntity.Make
                            (
                                guid: TestConstantsDm.SwitchablePoolEntityGuid,
                                name: TestConstantsDm.SwitchablePoolEntityName,
                                description: TestConstantsDm.SwitchablePoolEntityDescription,
                                switchableRepo: TestSwitchable.TheSwitchableRepo
                            )
                    );
            }
        }

        public static ISwitchablePoolEntity TestSwitchablePoolEntity(int repoSeed)
        {
            return
                    SwitchablePoolEntity.Make
                    (
                        guid: Guid.NewGuid(),
                        name: TestConstantsDm.SwitchablePoolEntityName,
                        description: TestConstantsDm.SwitchablePoolEntityDescription,
                        switchableRepo: TestSwitchable.SwitchableRepo(repoSeed)
                    );
        }

        #endregion

    }
}
