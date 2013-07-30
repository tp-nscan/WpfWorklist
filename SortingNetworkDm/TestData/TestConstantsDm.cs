using System;

namespace SortingNetworkDm.TestData
{
    public static class TestConstantsDm
    {
        public static string SorterPoolEntityName = "The Sorter Pool Entity Name";
        public static string SorterPoolEntityDescription = "The Sorter Pool Entity Description";
        public static Guid SorterPoolEntityGuid = Guid.NewGuid();

        public static string SorterResultPoolEntityName = "The Sorter Result Pool Entity Name";
        public static string SorterResultPoolEntityDescription = "The Sorter Result Pool Entity Description";
        public static Guid SorterResultPoolEntityGuid = Guid.NewGuid();

        public static string SwitchablePoolEntityName = "The Switchable Pool Entity Name";
        public static string SwitchablePoolEntityDescription = "The Switchable Pool Entity Description";
        public static Guid SwitchablePoolEntityGuid = Guid.NewGuid();



        public static string SorterPoolStepName = "The Sorter Pool Step Name";
        public static string SorterPoolStepDescription = "The Sorter Pool Step Description";

        public static string SwitchablePoolStepName = "The Switchable Pool Step Name";
        public static string SwitchablePoolStepDescription = "The Switchable Pool Step Description";

        public static string CompetePoolStepName = "The Compete Pool Step Name";
        public static string CompetePoolStepDescription = "The Compete Pool Step Description";
        public static Guid CompetePoolStepGuid = Guid.NewGuid();

        public static int SorterChampCount = 5;
        public static int SwitchableChampCount = 20;
        public static double MutationRate = 0.1;
        public static int NumGenerations = 3;

        public static int SorterPoolStepIndex = 0;
        public static int SwitchablePoolStepIndex = 1;
        public static int CompetePoolStepIndex = 2;
    }
}
