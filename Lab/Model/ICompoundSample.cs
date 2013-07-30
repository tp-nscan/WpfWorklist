using Lab.Model.Sample;

namespace Lab.Model
{
    public interface ICompoundSample : ISample
    {
        string CompoundName { get; }
    }
}
