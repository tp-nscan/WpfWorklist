using Lab.Model.Containers;
using Lab.Model.Containers.Plate;

namespace Lab.Model.PlateMap
{
    public class PlateMapTarget
    {
        public IContainer SampleSource { get; set; }
        public int SourceIndex { get; set; }
        public IWellLoc TargetWell { get; set; }
        public string TargetDescription { get; set; }
    }
}
