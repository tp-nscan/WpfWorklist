using System.Collections.Generic;
using System.Windows.Media;

namespace SampleControls.Model
{
    public class BrushFactory
    {
        static readonly Brush BlueBrush = new SolidColorBrush(Colors.Blue);
        static readonly Brush YellowBrush = new SolidColorBrush(Colors.Yellow);
        static readonly Brush GreenBrush = new SolidColorBrush(Colors.Green);
        static readonly Brush OrangeBrush = new SolidColorBrush(Colors.Orange);
        static readonly Brush RedBrush = new SolidColorBrush(Colors.Red);

        public static IEnumerable<Brush> Brushes
        {
            get
            {
                BrushFactory.BlueBrush.Freeze();
                BrushFactory.YellowBrush.Freeze();
                BrushFactory.GreenBrush.Freeze();
                BrushFactory.OrangeBrush.Freeze();
                BrushFactory.RedBrush.Freeze();

                for (;true;)
                {
                    yield return BlueBrush;
                    yield return GreenBrush;
                    yield return YellowBrush;
                    yield return OrangeBrush;
                    yield return RedBrush;
                }
            }

        }

    }
}
