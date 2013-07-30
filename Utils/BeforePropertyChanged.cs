namespace Utils
{
    public class BeforePropertyChanged<T>
    {
        public BeforePropertyChanged(T propertyHost, string propertyName, object newPropertyValue)
        {
            _propertyHost = propertyHost;
            _propertyName = propertyName;
            _newPropertyValue = newPropertyValue;
        }

        public bool Cancel { get; set; }

        private readonly T _propertyHost;
        public T PropertyHost
        {
            get { return _propertyHost; }
        }

        private readonly string _propertyName;
        public string PropertyName
        {
            get { return _propertyName; }
        }

        private readonly object _newPropertyValue;
        public object NewPropertyValue
        {
            get { return _newPropertyValue; }
        }
    }
}
