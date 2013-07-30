namespace Lab.Model.Containers.Plate
{
    public struct WellLoc : IWellLoc
    {
        public static WellLoc Empty = new WellLoc(null, null, "empty", SamplePlateSize.None);

        public WellLoc(string wellLabel, string samplePlateName, SamplePlateSize samplePlateSize)
        {
            _samplePlateName = samplePlateName;
            _samplePlateSize = samplePlateSize;
            _column = wellLabel.WellColumn();
            _row = wellLabel.WellRow();
            _isNull = false;
        }

        public WellLoc(int? row, int? column, string samplePlateName, SamplePlateSize samplePlateSize)
        {
            _row = row;
            _column = column;
            _samplePlateName = samplePlateName;
            _samplePlateSize = samplePlateSize;
            _isNull = false;
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

        private readonly string _samplePlateName;
        public string SamplePlateName
        {
            get { return _samplePlateName; }
        }

        private readonly SamplePlateSize _samplePlateSize;
        public SamplePlateSize SamplePlateSize
        {
            get { return _samplePlateSize; }
        }

        #region Implementation of INullable

        /// <summary>
        /// Indicates whether a structure is null. This property is read-only.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Data.SqlTypes.SqlBoolean"/>true if the value of this object is null. Otherwise, false.
        /// </returns>
        private readonly bool? _isNull;
        public bool IsNull
        {
            get { return _isNull == null; }
        }

        #endregion

        public ContainerType ContainerType
        {
            get { return ContainerType.Well;}
        }

        public string LocId
        {
            get
            {
                return string.Format(
                    "{0}_{1}_{2}",
                    (SamplePlateSize == SamplePlateSize.Size96) ? "w" : "W",
                    SamplePlateName,
                    this.LongPlateCoords());
            }
        }
    }

}
