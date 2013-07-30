using System;
using Lab.Model.Sample;

namespace Lab.Model.Containers
{
    public interface IContainer : IContainerLoc
    {
        event EventHandler SampleAdded;
        ISample Sample { get; set; }
    }
}
