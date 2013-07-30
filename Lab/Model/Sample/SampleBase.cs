using Lab.Model.Containers;

namespace Lab.Model.Sample
{
    public class SampleBase : ISample
    {
        public SampleBase(IContainerLoc containerLoc, string id)
        {
            _containerLoc = containerLoc;
            _id = id;
        }

        #region Implementation of ISample

        private readonly IContainerLoc _containerLoc;
        public IContainerLoc Location
        {
            get { return _containerLoc; }
        }

        private readonly string _id;
        public string Id
        {
            get { return _id; }
        }

        #endregion
    }
}
