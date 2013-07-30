using DynamicModel.Common;
using SortNetwork.Results;
using SortNetwork.Switchables;

namespace SortingNetworkDm.Steps
{
    public interface ICompetePoolRunMessage : IRunMessage
    {
        int Generation { get; }
        ISorterResultRepo SorterResultRepo { get; }
        ISwitchableRepo SwitchableRepo { get; }
    }

    public static class CompetePoolRunMessage
    {
        public static string TypeName = typeof(CompetePoolRunMessage).Name;

        public static ICompetePoolRunMessage Make
            (
                int generation,
                int seedOut,
                ISorterResultRepo sorterResultRepo,
                ISwitchableRepo switchableRepo,
                string message = null
            )
        {
            return new CompetePoolRunMessageImpl
                (
                    generation: generation,
                    seedOut: seedOut,
                    sorterResultRepo: sorterResultRepo,
                    switchableRepo: switchableRepo,
                    message: message
                );
        }
    }

    class CompetePoolRunMessageImpl : ICompetePoolRunMessage
    {
        public CompetePoolRunMessageImpl
            (
                int generation,
                int seedOut,
                ISorterResultRepo sorterResultRepo, 
                ISwitchableRepo switchableRepo, 
                string message
            )
        {
            _generation = generation;
            _seedOut = seedOut;
            _sorterResultRepo = sorterResultRepo;
            _switchableRepo = switchableRepo;
            _message = message;
        }

        private readonly int _generation;
        public int Generation
        {
            get { return _generation; }
        }

        private readonly ISorterResultRepo _sorterResultRepo;
        public ISorterResultRepo SorterResultRepo
        {
            get { return _sorterResultRepo; }
        }

        private readonly ISwitchableRepo _switchableRepo;
        public ISwitchableRepo SwitchableRepo
        {
            get { return _switchableRepo; }
        }

        private readonly string _message;
        public string Message
        {
            get { return _message; }
        }

        private readonly int _seedOut;
        public int SeedOut
        {
            get { return _seedOut; }
        }

        public string Type
        {
            get { return CompetePoolRunMessage.TypeName; }
        }
    }
}
