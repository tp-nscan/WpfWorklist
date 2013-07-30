namespace SampleControls.Model.Legend
{
    public class BrushForProperty
    {
        public BrushForProperty(string propertyName, IBrushMap brushMap)
        {
            _propertyName = propertyName;
            _brushMap = brushMap;
        }

        private readonly string _propertyName;
        public string PropertyName
        {
            get { return _propertyName; }
        }

        private readonly IBrushMap _brushMap;
        public IBrushMap BrushMap
        {
            get { return _brushMap; }
        }
    }
}
