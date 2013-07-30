using System;

namespace Utils
{
    public interface INamedItem
    {
        string Name { get; }
        IObservable<BeforePropertyChanged<INamedItem>> OnNameChanging { get; }
    }
}
