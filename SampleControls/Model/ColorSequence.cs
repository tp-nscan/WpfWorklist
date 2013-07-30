using System.Collections.Generic;
using System.Windows.Media;
using MathUtils.Interval;

namespace SampleControls.Model
{
    public class ColorSequence
    {
        public ColorSequence(Color lowColor, Color hiColor, int stepCount)
        {
            _lowColor = lowColor;
            _hiColor = hiColor;
            _stepCount = stepCount;
            MakeColorRange();
        }

        void MakeColorRange()
        {
            ColorSteps.Clear();

            for (var i = 0; i < StepCount; i++)
            {
                ColorSteps.Add( new Color()
                {
                    ScA = RealIntervalExt.GetTicValue(LowColor.ScA, HiColor.ScA, i, StepCount + 1),
                    ScR = RealIntervalExt.GetTicValue(LowColor.ScR, HiColor.ScR, i, StepCount + 1),
                    ScG = RealIntervalExt.GetTicValue(LowColor.ScG, HiColor.ScG, i, StepCount + 1),
                    ScB = RealIntervalExt.GetTicValue(LowColor.ScB, HiColor.ScB, i, StepCount + 1)
                });
            }
        }

        public Color GetColor(int index)
        {
            return ColorSteps[index];
        }

        private readonly Color _lowColor;
        private Color LowColor
        {
            get { return _lowColor; }
        }

        private readonly Color _hiColor;
        private Color HiColor
        {
            get { return _hiColor; }
        }

        private readonly int _stepCount;
        public int StepCount
        {
            get { return _stepCount; }
        }

        protected List<Color> ColorSteps;
    }
}
