using System.Collections.ObjectModel;
using System.Linq;
using MathUtils.Collections;
using SortNetwork.Results;
using WpfUtils;

namespace SorterControls.ViewModels.Entities
{
    public interface ISorterResultVm
    {
        ObservableCollection<SwitchResultVm> SwitchResultVms { get; set; }
        int SwitchesUsed { get; }
        int TotalSwitches { get; }
    }

    public static class SorterResultVm
    {
        public static ISorterResultVm Make(ISorterResult sorterResult)
        {
            return new SorterResultVmImpl(sorterResult);
        }
    }

    class SorterResultVmImpl : ViewModelBase, ISorterResultVm
    {
        public SorterResultVmImpl(ISorterResult sorterResult)
        {
            _sorterResult = sorterResult;

            foreach (var switchResult in sorterResult.SwitchResults.NotNull())
            {
                SwitchResultVms.Add
                    (
                        new SwitchResultVm
                                (
                                    switchResult,
                                    useFraction: (sorterResult.CountOfTests > 0) ? (double)switchResult.UseCount / (double)sorterResult.CountOfTests : 0
                                )
                   );
            }
        }

        private readonly ISorterResult _sorterResult;

        public ISorterResult SorterResult
        {
            get { return _sorterResult; }
        }

        private ObservableCollection<SwitchResultVm> _switchResultVms 
            = new ObservableCollection<SwitchResultVm>();
        public ObservableCollection<SwitchResultVm> SwitchResultVms
        {
            get { return _switchResultVms; }
            set { _switchResultVms = value; }
        }

        public int SuccessfulSorts
        {
            get { return SorterResult.SuccessfulSorts; }
        }

        public int SwitchesUsed
        {
            get { return SorterResult.SwitchesUsed; }
        }

        public int TotalSwitches
        {
            get { return SorterResult.SwitchResults.Count() ; }
        }
    }
}
