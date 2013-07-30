using System;
using Lab.Model.Sample;

namespace Lab.Model.Containers
{
    public abstract class Container : IContainer
    {
        protected Container()
        {
            _isNull = false;
        }

        public event EventHandler SampleAdded;
        void OnSampleAdded(EventArgs e)
        {
            EventHandler handler = SampleAdded;
            if (handler != null) handler(this, e);
        }

        public abstract ContainerType ContainerType { get; }

        public abstract string LocId { get; }

        private ISample _sample;
        public ISample Sample
        {
            get { return _sample; }
            set
            {
                if(_sample!=null)
                {
                    throw new Exception("Sample already added");
                }
                if(ContainerType==ContainerType.Empty)
                {
                    throw new Exception("Cannot add sample to an empty container type");
                }
                _sample = value; 
                OnSampleAdded(EventArgs.Empty);
            }
        }

        public string SampleName { get; set; }

        public static IContainer Empty = new EmptyContainer();

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
    }

    public class EmptyContainer : Container
    {
        public override ContainerType ContainerType
        {
            get { return ContainerType.Empty; }
        }

        public override string LocId
        {
            get { return "empty"; }
        }
    }

}
