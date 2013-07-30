namespace Lab.Model.Containers.Plate
{
    /// <summary>
    /// Information about how to find a well
    /// </summary>
    public interface IWellLoc : IContainerLoc
    {
        /// <summary>
        /// 0 based index
        /// </summary>
        int? Column { get; }
        /// <summary>
        /// 0 based index
        /// </summary>
        int? Row { get; }

        string SamplePlateName { get; }
        SamplePlateSize SamplePlateSize { get; }
    }

}
