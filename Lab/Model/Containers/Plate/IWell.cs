namespace Lab.Model.Containers.Plate
{
    /// <summary>
    /// A model of an actual well
    /// </summary>
    public interface IWell : IContainer, IWellLoc
    {
        ISamplePlate SamplePlate { get; }
    }

}
