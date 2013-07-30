using System.Windows.Media;

namespace SampleControls.Model
{
    public interface IBrushMap
    {
        Brush GetBrush(object value);
    }
}
