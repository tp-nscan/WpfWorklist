using System;
using System.Linq;
using System.Collections.Generic;

namespace MathUtils.SortableUtils
{
    public static class SortableBitArrayUtils
    {
        public static int CompareDict(this bool[] bits, bool[] comp, int size)
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (bits[i] != comp[i])
                {
                    return bits[i] ? 1 : -1;
                }
            }
            return 0;
        }

        public static bool IsSorted(this bool[] bits, int size)
        {
            var foundAFalse = false;
            for (var i = size - 1; i >= 0; i--)
            {
                if (bits[i])
                {
                    if (foundAFalse)
                    {
                        return false;
                    }
                }
                else
                {
                    foundAFalse = true;
                }
            }
            return true;
        }

        public static IEnumerable<bool> Xor(this IEnumerable<bool> bits, IEnumerable<bool> otherBits)
        {
            return bits.Zip(otherBits, (a, b) => a^b );
        }

        public static IEnumerable<bool> FlipWhen(this IEnumerable<bool> bits, IEnumerable<bool> flipBits)
        {
            return bits.Zip(flipBits, (a, b) => b ? !a : a);
        }

        public static IEnumerable<bool> ToBools(this byte data)
        {
            yield return (data & 1) > 0;
            yield return (data & 2) > 0;
            yield return (data & 4) > 0;
            yield return (data & 8) > 0;
            yield return (data & 16) > 0;
            yield return (data & 32) > 0;
            yield return (data & 64) > 0;
            yield return (data & 128) > 0;
        }

        public static IEnumerable<bool> ToBools(this ushort data, int length)
        {
            if (length > 16)
            {
                throw new Exception("Length too long");
            }
            for (var i = 0; i < length; i++)
            {
                yield return (data & UshortPowersOfTwo[i]) > 0;
            }
        }

        public static IEnumerable<bool> ToBools(this uint data, int length)
        {
            if (length > 32)
            {
                throw new Exception("Length too long");
            }
            for (var i = 0; i < length; i++)
            {
                yield return (data & UIntPowersOfTwo[i]) > 0;
            }
        }

        static readonly ushort[] UshortPowersOfTwo = new ushort[]
            {
                1,
                2,
                4,
                8,
                16,
                32,
                64,
                128,
                256,
                512,
                1024,
                2048,
                4096,
                8192,
                16384,
                32768
            };

        private static readonly uint[] UIntPowersOfTwo = new uint[]
            {
                1,
                2,
                4,
                8,
                16,
                32,
                64,
                128,
                256,
                512,
                1024,
                2048,
                4096,
                8192,
                16384,
                32768,
                65536,
                131072,
                262144,
                524288,
                1048576,
                2097152,
                4194304,
                8388608,
                16777216,
                33554432,
                67108864,
                134217728,
                268435456,
                536870912,
                1073741824,
                2147483648
            };

        public static IEnumerable<bool[]> AllBitSetsOfLength(int length)
        {
            if (length <= 16)
            {
                foreach (var @ushort in Enumerable.Range(0, (int) Math.Pow(2, length)))
                {
                    yield return ((ushort)@ushort).ToBools(length).ToArray();
                }
                yield break;
            }
            if (length <= 32)
            {
                foreach (var @uint in Enumerable.Range(0, (int)Math.Pow(2, length)))
                {
                    yield return ((uint)@uint).ToBools(length).ToArray();
                }
                yield break;
            }
            throw new Exception("length too long");
        }

        public static byte ToByte(this bool[] bits)
        {
            if (bits.Length > 8)
            {
                throw new ArgumentException("array length must be <= 8");
            }
            byte bRet = 0;
            for (int i = bits.Length -1; i > -1; i--)
            {
                bRet <<= 1;
                if (bits[i]) bRet += 1;
            }
            return bRet;
        }

        public static ushort ToUshort(this bool[] bits)
        {
            if (bits.Length > 16)
            {
                throw new ArgumentException("array length must be <= 16");
            }
            ushort bRet = 0;
            for (int i = bits.Length - 1; i > -1; i--)
            {
                bRet <<= 1;
                if (bits[i]) bRet += 1;
            }
            return bRet;
        }
    }
}
