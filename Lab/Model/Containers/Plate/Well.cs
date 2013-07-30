namespace Lab.Model.Containers.Plate
{
    public class Well : Container, IWell
    {
        public Well(string wellLabel)
        {
            _column = wellLabel.WellColumn();
            _row = wellLabel.WellRow();
        }

        public Well(int row, int column)
        {
            _row = row;
            _column = column;
        }

        private readonly int? _column;
        public int? Column
        {
            get { return _column; }
        }

        private readonly int? _row;
        public int? Row
        {
            get { return _row; }
        }

        public string SamplePlateName
        {
            get { return SamplePlate.Name; }
        }

        public SamplePlateSize SamplePlateSize
        {
            get { return SamplePlate.SamplePlateSize; }
        }

        public ISamplePlate SamplePlate { get; set; }

        public override ContainerType ContainerType
        {
            get { return ContainerType.Well; }
        }

        public override string LocId
        {
            get
            {

                return string.Format(
                    "{0}_{1}_{2}", 
                    (SamplePlateSize==SamplePlateSize.Size96) ? "w":"W",  
                    SamplePlateName, 
                    this.LongPlateCoords());
            }
        }
    }

}
