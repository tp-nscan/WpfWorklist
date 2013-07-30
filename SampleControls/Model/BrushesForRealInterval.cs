using System;
using System.Collections.Generic;
using System.Windows.Media;
using MathUtils.Map;

namespace SampleControls.Model
{
    public class BrushesForRealInterval : IBrushMap
    {
        public BrushesForRealInterval(Color lowColor, Color hiColor, OrderedRangeMap orderedRangeMap)
        {
            _colorSequence = new ColorSequence(lowColor, hiColor, OrderedRangeMap.PartitionCount + 3);
            _orderedRangeMap = orderedRangeMap;
            _brushList = new List<Brush>();

            for(var i=0; i < ColorSequence.StepCount; i++)
            {
                BrushList.Add(new SolidColorBrush(ColorSequence.GetColor(i)));
            }
        }

        private readonly ColorSequence _colorSequence;
        private ColorSequence ColorSequence
        {
            get { return _colorSequence; }
        }

        private readonly OrderedRangeMap _orderedRangeMap;
        private OrderedRangeMap OrderedRangeMap
        {
            get { return _orderedRangeMap; }
        }

        private readonly List<Brush> _brushList;
        private List<Brush> BrushList
        {
            get { return _brushList; }
        }

        public Brush GetBrush(double value)
        {
            return BrushList[OrderedRangeMap.IndexOfValue(value)];
        }

        private readonly Brush _errorBrush = Brushes.Pink;
        public Brush GetBrush(object value)
        {
            try
            {
                return GetBrush((double)value);
            }
            catch (Exception)
            {
                return _errorBrush;
            }
        }

    }
}
