namespace Lab.Model.Containers.Single
{
    public struct BottleLoc : IBottleLoc
    {
        public static BottleLoc Empty = new BottleLoc("empty");

        public BottleLoc(string locId)
        {
            _locId = locId;
            _isNull = false;
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
            get { return ContainerType.Bottle; }
        }

        private readonly string _locId;
        public string LocId
        {
            get { return _locId; }
        }

        public string Name
        {
            get { return LocId; }
        }
    }
}
