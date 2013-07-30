namespace MathUtils.SortableUtils
{
    public static class SortableIntUtils
    {
        public static bool BitValue(this ushort value, int bitPos)
        {
            return (value & 1 << bitPos) > 0;
        }

        public static bool BitValue(this uint value, int bitPos)
        {
            return (value & ((uint)1) << bitPos) > 0;
        }

        public static bool BitValue(this ulong value, int bitPos)
        {
            return (value & ((ulong)1) << bitPos) > 0;
        }

        public static ushort FilpBit(this ushort value, int bitPos)
        {
            return (ushort) (value ^ 1 << bitPos);
        }

        public static uint FilpBit(this uint value, int bitPos)
        {
            return value ^ ((uint)1) << bitPos;
        }

        public static bool[] ToBits(this ushort value, int numBits)
        {
            var retVal = new bool[numBits];

            for (int i = 0; i < numBits; i++)
            {
                retVal[i] = value.BitValue(i);
            }
            return retVal;
        }
        public static bool[] ToBits(this uint value, int numBits)
        {
            var retVal = new bool[numBits];

            for (int i = 0; i < numBits; i++)
            {
                retVal[i] = value.BitValue(i);
            }
            return retVal;
        }

        public static bool[] ToBits(this ulong value, int numBits)
        {
            var retVal = new bool[numBits];

            for (int i = 0; i < numBits; i++)
            {
                retVal[i] = value.BitValue(i);
            }
            return retVal;
        }

        public static ushort SortBitPair(this ushort value, int lowBit, int hiBit)
        {
            var hasLowBit = value.BitValue(lowBit);
            var hasHighBit = value.BitValue(hiBit);

            if (hasLowBit == hasHighBit) { return value; }
            if (hasHighBit) { return value; }

            return value.FilpBit(lowBit).FilpBit(hiBit);
        }
    }
}
