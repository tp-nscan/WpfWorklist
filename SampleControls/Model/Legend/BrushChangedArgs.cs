using System;
using System.Windows.Media;

namespace SampleControls.Model.Legend
{
    public class BrushChangedArgs : EventArgs
    {
        public BrushChangedArgs(Brush brush)
        {
            _brush = brush;
        }

        private readonly Brush _brush;
        public Brush Brush
        {
            get { return _brush; }
        }
    }
}
