using System.Collections.ObjectModel;
using DynamicModel.Model;
using DynamicModel.ViewModel;
using SortingNetworkDm.Entities;

namespace SorterControls.ViewModels.Entities
{
    public interface ISorterPoolVm : IEntityVm
    {
        ISorterPoolEntity SorterPoolEntity { get; }
        ObservableCollection<SorterVm> SorterVms { get; }
        int SorterCount { get; }
        IEntity Entity { get; }
    }

    public static class SorterPoolVm
    {
        public static ISorterPoolVm Make(ISorterPoolEntity sorterPoolEntity)
        {
            return new SorterPoolVmImpl(sorterPoolEntity);
        }
    }

    public class SorterPoolVmImpl : EntityVmImpl, ISorterPoolVm
    {
        public SorterPoolVmImpl(ISorterPoolEntity sorterPoolEntity)
            : base(sorterPoolEntity)
        {
            foreach (var sorter in SorterPoolEntity.SorterRepo)
            {
                SorterVms.Add(new SorterVm(sorter));
            }
        }

        public ISorterPoolEntity SorterPoolEntity
        {
            get { return (ISorterPoolEntity) Entity; }
        }

        private readonly ObservableCollection<SorterVm> _sorterVms = new ObservableCollection<SorterVm>();
        public ObservableCollection<SorterVm> SorterVms
        {
            get { return _sorterVms; }
        }

        public int SorterCount
        {
            get { return SorterPoolEntity.SorterRepo.Count; }
        }

        public override string TypeName
        {
            get { return SorterPoolEntity.TypeName; }
        }
    }
}