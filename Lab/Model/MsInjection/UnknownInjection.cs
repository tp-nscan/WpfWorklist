using System;
using System.Data;
using Lab.Model.Containers;

namespace Lab.Model.MsInjection
{
    public class UnknownInjection : IInjection
    {
        public string AssociatedPlateName
        {
            get { return string.Empty; }
        }


        public IObservable<IInjection> OnContainerAssigned { get { return null; } }
        public ISamplePlate AssociatedPlate
        {
            get { return null; }
            set {throw new NoNullAllowedException("cant assign a plate to the unknown injection");}
        }

        public IContainerLoc Source
        {
            get { return Container.Empty; }
        }

        public string Id
        {
            get { return string.Format("{0}_{1}_{2}", Source.LocId, AssociatedPlateName, Index); }
        }

        static UnknownInjection _UnknownInjection = new UnknownInjection();

        public static string UnknownInjectionId
        {
            get { return _UnknownInjection.Id; }
        }

        public int Index
        {
            get { return 0; }
        }

        public InjectionType InjectionType
        {
            get { return InjectionType.Unknown; }
        }
    }
}
