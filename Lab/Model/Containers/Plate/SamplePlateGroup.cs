using System;
using System.Linq;
using Utils;

namespace Lab.Model.Containers.Plate
{
    public class SamplePlateGroup : UniqueGroupOfNames<ISamplePlate>
    {
        public SamplePlateGroup(SamplePlateSize samplePlateSize)
        {
            _samplePlateSize = samplePlateSize;
        }

        private readonly SamplePlateSize _samplePlateSize;
        public SamplePlateSize SamplePlateSize
        {
            get { return _samplePlateSize; }
        }

        public override bool AddItem(ISamplePlate item)
        {
            if ((SamplePlateSize != SamplePlateSize.None) && (item.SamplePlateSize != SamplePlateSize))
            {
                throw new Exception(String.Format("SamplePlateSize mismatch in AddItem"));
            }
            return base.AddItem(item);
        }

        public ISamplePlate GetOrMakeSamplePlate(string samplePlateName)
        {
            var samplePlate = Items.SingleOrDefault((T => T.Name == samplePlateName));
            if (samplePlate != null)
            {
                return samplePlate;
            }
            if (SamplePlateSize == SamplePlateSize.None)
            {
                throw new Exception("SamplePlateSize not specified in GetOrMakeSamplePlate");
            }
            samplePlate = new SamplePlate(SamplePlateSize, samplePlateName);
            AddItem(samplePlate);
            return samplePlate;
        }

        public ISamplePlate GetOrMakeSamplePlate(string samplePlateName, SamplePlateSize samplePlateSize)
        {
            if ((samplePlateSize == SamplePlateSize.None) || (samplePlateSize != SamplePlateSize))
            {
                throw new Exception("Incorrect SamplePlateSize in GetOrMakeSamplePlate");
            }

            var samplePlate = Items.SingleOrDefault(T => T.Name == samplePlateName);
            if (samplePlate != null)
            {
                return samplePlate;
            }

            samplePlate = new SamplePlate(SamplePlateSize, samplePlateName);
            AddItem(samplePlate);
            return samplePlate;
        }

    }
}
