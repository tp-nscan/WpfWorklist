using System;
using System.Windows.Media;

namespace SampleControls.Model
{
    public class BrushesForBool : IBrushMap
    {
        public BrushesForBool(Brush falseBrush, Brush trueBrush)
        {
            _falseBrush = falseBrush;
            _trueBrush = trueBrush;
        }

        public Brush BrushForValue(bool value)
        {
            return value ? TrueBrush : FalseBrush;
        }

        private readonly Brush _falseBrush;
        public Brush FalseBrush
        {
            get { return _falseBrush; }
        }

        private readonly Brush _trueBrush;
        public Brush TrueBrush
        {
            get { return _trueBrush; }
        }

        private readonly Brush _errorBrush = Brushes.Pink;
        public Brush GetBrush(object value)
        {
            try
            {
                return BrushForValue((bool) value);
            }
            catch (Exception)
            {
                return _errorBrush;
            }
        }
    }
}
