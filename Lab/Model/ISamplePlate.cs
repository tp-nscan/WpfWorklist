using System;
using System.Collections.Generic;
using Lab.Model.Containers.Plate;
using Utils;

namespace Lab.Model
{
    public interface ISamplePlate : INamedItem
    {
        SamplePlateSize SamplePlateSize { get; }
        IObservable<ISamplePlate> OnNameChanged { get; }
        IWell Well(int row, int column);
        IEnumerable<IWell> Wells { get; }
    }

    public static class SamplePlateExt
    {
        public static int RowCount(this SamplePlateSize samplePlateSize)
        {
            switch (samplePlateSize)
            {
                case SamplePlateSize.Size96:
                    return 8;
                case SamplePlateSize.Size384:
                    return 16;
                default:
                    throw new ArgumentException("SamplePlate size not handled");
            }
        }

        public static int ColumnCount(this SamplePlateSize samplePlateSize)
        {
            switch (samplePlateSize)
            {
                case SamplePlateSize.Size96:
                    return 12;
                case SamplePlateSize.Size384:
                    return 24;
                default:
                    throw new ArgumentException("SamplePlate size not handled");
            }
        }
    }

}
