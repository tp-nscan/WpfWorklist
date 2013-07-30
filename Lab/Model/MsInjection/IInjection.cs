using System;
using Lab.Model.Containers;
using Lab.Model.Containers.Plate;

namespace Lab.Model.MsInjection
{
    public interface IInjection
    {
        ISamplePlate AssociatedPlate { get; set; }
        IObservable<IInjection> OnContainerAssigned { get; }
        IContainerLoc Source { get; }
        string Id { get; }
        int Index { get; }
        InjectionType InjectionType { get; }
    }
}
