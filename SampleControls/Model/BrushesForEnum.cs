using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SampleControls.Model
{
    public abstract class BrushesForEnum<T> : IBrushMap
    {
        public abstract IEnumerable<EnumBrushPair<T>> EnumBrushPairs { get; }

        private readonly Brush _errorBrush = Brushes.Pink;
        public Brush GetBrush(object value)
        {
            try
            {
                return EnumBrushPairs.Single(v => v.Value.Equals((T)value)).Brush;
            }
            catch (Exception)
            {
                return _errorBrush;
            }
        }
    }

    public class EnumBrushPair<T>
    {
        public T Value { get; set; }
        public Brush Brush { get; set; }
    }
}
