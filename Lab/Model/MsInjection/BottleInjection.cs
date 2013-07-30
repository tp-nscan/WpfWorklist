using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Lab.Model.Containers;
using Lab.Model.Containers.Plate;
using Lab.Model.Containers.Single;

namespace Lab.Model.MsInjection
{
    public class BottleInjection : IInjection
    {
        public BottleInjection(IBottleLoc bottleLoc, int index, ISamplePlate associatedPlate)
        {
            AssociatedPlate = associatedPlate;
            _index = index;
            _bottleLoc = bottleLoc;
        }

        public InjectionType InjectionType
        {
            get { return InjectionType.Bottle; }
        }

        private readonly Subject<IInjection> _onContainerAssigned = new Subject<IInjection>();
        public IObservable<IInjection> OnContainerAssigned { get { return _onContainerAssigned.AsObservable(); } }
        private ISamplePlate _associatedPlate; 
        private IDisposable _associatedPlateSubscription;
        public ISamplePlate AssociatedPlate
        {
            get { return _associatedPlate; }
            set
            {
                if (_associatedPlateSubscription != null)
                {
                    _associatedPlateSubscription.Dispose();
                }
                _associatedPlate = value;
                _associatedPlateSubscription = _associatedPlate.OnNameChanged.Subscribe(p => _onContainerAssigned.OnNext(this));
            }
        }

        public string Id
        {
            get
            {
                if (AssociatedPlate.SamplePlateSize == SamplePlateSize.Size384)
                {
                    return string.Format("B_{0}_{1}_{2}", _bottleLoc.Name, AssociatedPlate.Name, Index);
                }
                return string.Format("{0}_{1}_{2}", Source.LocId, AssociatedPlate.Name, Index);
            }
        }

        private readonly int _index;
        public int Index
        {
            get { return _index; }
        }

        private readonly IBottleLoc _bottleLoc;
        public IContainerLoc Source
        {
            get { return _bottleLoc; }
        }
    }
}
