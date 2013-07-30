using System.Collections.ObjectModel;
using DynamicModel.ViewModel;
using WpfUtils;

namespace SorterControls.DesignData.Steps
{
    public class DesignEntitiesVm : ViewModelBase
    {

        public DesignEntitiesVm()
        {
            EntityVms.Add(EntityVm.MakeTest("entity name 1"));
            EntityVms.Add(EntityVm.MakeTest("entity name 2"));
        }

        private ObservableCollection<IEntityVm> _entityVms 
            = new ObservableCollection<IEntityVm>();
        public ObservableCollection<IEntityVm> EntityVms
        {
            get { return _entityVms; }
            set { _entityVms = value; }
        }
    }
}
