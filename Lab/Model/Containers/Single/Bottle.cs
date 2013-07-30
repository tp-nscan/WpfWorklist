namespace Lab.Model.Containers.Single
{
    public class Bottle : Container, IBottleLoc
    {
        public Bottle(string name)
        {
            _name = name;
        }

        private readonly string _name;
        public string Name
        {
            get { return _name; }
        }

        public override ContainerType ContainerType
        {
            get { return ContainerType.Bottle; }
        }

        public override string LocId
        {
            get { return string.Format("b_{0}", Name); }
        }
    }
}
