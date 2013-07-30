using System;
using System.Collections.ObjectModel;
using DynamicModel.ViewModel;
using SortingNetworkDm.Entities;

namespace SorterControls.ViewModels.Entities
{
    public interface ISwitchablePoolVm : IEntityVm
    {
        int SwitchableCount { get; }
        ISwitchablePoolEntity SwitchablePoolEntity { get; }
        ObservableCollection<SwitchableVm> SwitchableVms { get; }
    }

    public static class SwitchablePoolVm
    {
        public static ISwitchablePoolVm Make(ISwitchablePoolEntity switchablePoolEntity)
        {
            return new SwitchablePoolVmImpl(switchablePoolEntity);
        }
    }

    public class SwitchablePoolVmImpl : EntityVmImpl, ISwitchablePoolVm
    {
        public SwitchablePoolVmImpl(ISwitchablePoolEntity switchablePoolEntity)
            : base(switchablePoolEntity)
        {
            foreach (var switchable in SwitchablePoolEntity.SwitchableRepo)
            {
                SwitchableVms.Add(new SwitchableVm(switchable));
            }
        }

        public ISwitchablePoolEntity SwitchablePoolEntity
        {
            get { return (ISwitchablePoolEntity) Entity; }
        }

        private readonly ObservableCollection<SwitchableVm> _switchableVms 
            = new ObservableCollection<SwitchableVm>();

        public ObservableCollection<SwitchableVm> SwitchableVms
        {
            get { return _switchableVms; }
        }

        public int SwitchableCount
        {
            get { return SwitchablePoolEntity.SwitchableRepo.Count; }
        }

        public override string TypeName
        {
            get { return SwitchablePoolEntity.TypeName; }
        }
    }
}
