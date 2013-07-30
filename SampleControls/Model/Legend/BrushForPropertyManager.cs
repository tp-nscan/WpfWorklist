using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace SampleControls.Model.Legend
{
    public class BrushForPropertyManager
    {
        public BrushForPropertyManager(INotifyPropertyChanged propertyHost)
        {
            _propertyHost = propertyHost;
            _propertyHost.PropertyChanged += PropertyHostPropertyChanged;
        }

        void PropertyHostPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateBrush(e.PropertyName);
        }

        private readonly INotifyPropertyChanged _propertyHost;
        private INotifyPropertyChanged PropertyHost
        {
            get { return _propertyHost; }
        }

        public event EventHandler<BrushChangedArgs> BrushChanged;

        public void OnBrushChanged(BrushChangedArgs e)
        {
            EventHandler<BrushChangedArgs> handler = BrushChanged;
            if (handler != null) handler(this, e);
        }

        public void AddLegendProperty(BrushForProperty legendProperty)
        {
            LegendProperties[legendProperty.PropertyName] = legendProperty;
        }

        private readonly Dictionary<string, BrushForProperty> _legendProperties 
            = new Dictionary<string, BrushForProperty>();
        private Dictionary<string, BrushForProperty> LegendProperties
        {
            get { return _legendProperties; }
        }

        void UpdateBrush(string propertyName)
        {
            if (LegendProperties.ContainsKey(propertyName) && (ActiveProperty==propertyName))
            {
                Brush newBrush;
                try
                {
                    var hostValue = PropertyHost.GetType().GetProperty(propertyName).GetValue(PropertyHost, null);
                    newBrush = LegendProperties[propertyName].BrushMap.GetBrush(hostValue);
                }
                catch
                {
                    newBrush = Brushes.Pink;
                }
                OnBrushChanged(new BrushChangedArgs(newBrush));
            }
        }

        private string _activeProperty;
        public string ActiveProperty
        {
            get { return _activeProperty; }
            set
            {
                _activeProperty = value;
                UpdateBrush(value);
            }
        }
    }
}
