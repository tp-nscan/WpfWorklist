using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Lab.Model.Containers;
using Lab.Model.Containers.Plate;

namespace Lab.Model.MsInjection
{
    public class WellInjection : IInjection
    {
        public WellInjection(IWellLoc sourceWell, int index, ISamplePlate associatedPlate)
        {
            _wellLoc = sourceWell;
            _index = index;
            AssociatedPlate = associatedPlate;
        }

        public InjectionType InjectionType
        {
            get { return InjectionType.Well; }
        }

        private readonly IWellLoc _wellLoc;
        public IWellLoc WellLoc
        {
            get { return _wellLoc; }
        }

        private readonly Subject<IInjection> _onContainerAssigned = new Subject<IInjection>();
        public IObservable<IInjection> OnContainerAssigned { get { return _onContainerAssigned.AsObservable(); } }
        private IDisposable _associatedPlateSubscription;
        private ISamplePlate _associatedPlate;

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

                if (_associatedPlate != null)
                {
                    _associatedPlateSubscription = _associatedPlate.OnNameChanged.Subscribe(p => _onContainerAssigned.OnNext(this));
                }
            }
        }

        public string Id
        {
            get
            {
                if (AssociatedPlate == null)
                {
                    return string.Format("{0}_{1}", WellLoc.LocId, Index);
                }
                return string.Format("{0}_{1}_{2}_{3}", (AssociatedPlate.SamplePlateSize == SamplePlateSize.Size96) ? "w" : "W",
                    AssociatedPlate.Name,
                    WellLoc.LongPlateCoords(), Index);
            }
        }

        private readonly int _index;
        public int Index
        {
            get { return _index; }
        }

        public IContainerLoc Source
        {
            get { return WellLoc; }
        }
    }
}
