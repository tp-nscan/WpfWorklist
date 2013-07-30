using System.Collections.ObjectModel;
using System.Linq;
using DynamicModel.Model;
using DynamicModel.ViewModel;
using SortingNetworkDm.Entities;
using WpfUtils;

namespace SorterControls.ViewModels.Entities
{
    public interface ISorterResultPoolVm : IEntityVm
    {
        IEntity Entity { get; }
        ISorterResultPoolEntity SorterResultPoolEntity { get; }
        ObservableCollection<ISorterResultVm> SorterResultVms { get; }
        int SorterResultCount { get; }
        int SwitchesPerSorterResult { get; }
    }

    public class SorterResultPoolVm : ViewModelBase
    {
        public static ISorterResultPoolVm Make(ISorterResultPoolEntity sorterPoolEntity)
        {
            return new SorterResultPoolVmImpl(sorterPoolEntity);
        }
    }

    public class SorterResultPoolVmImpl : EntityVmImpl, ISorterResultPoolVm
    {
        public SorterResultPoolVmImpl(ISorterResultPoolEntity sorterResultPoolEntity)
            : base(sorterResultPoolEntity)
        {
            _switchesPerSorterResult = 0;
            foreach (var sorter in SorterResultPoolEntity.SorterResultRepo)
            {
                _switchesPerSorterResult = sorter.SwitchResults.Count();
                SorterResultVms.Add(SorterResultVm.Make(sorter));
            }
        }

        public ISorterResultPoolEntity SorterResultPoolEntity
        {
            get { return (ISorterResultPoolEntity)Entity; }
            set { Entity = value; }
        }

        private readonly ObservableCollection<ISorterResultVm> _sorterResultVms
            = new ObservableCollection<ISorterResultVm>();

        public ObservableCollection<ISorterResultVm> SorterResultVms
        {
            get { return _sorterResultVms; }
        }

        public int SorterResultCount
        {
            get { return SorterResultPoolEntity.SorterResultRepo.Count; }
        }

        private readonly int _switchesPerSorterResult;
        public int SwitchesPerSorterResult
        {
            get { return _switchesPerSorterResult; }
        }

        public override string TypeName
        {
            get { return SorterResultPoolEntity.TypeName; }
        }
    }
}
