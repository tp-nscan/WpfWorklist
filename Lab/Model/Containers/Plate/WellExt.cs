using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab.Model.Containers.Plate
{
    public static class WellExt
    {
        static WellExt()
        {
            //enough labels for a 384 well plate
            RowLabels = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
            ColumnLabels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24" };
            ColumnLabelsL = new[] { "001", "002", "003", "004", "005", "006", "007", "008", "009", "010", "011", "012", "013", "014", "015", "016", "017", "018", "019", "020", "021", "022", "023", "024" };
        }

        private static readonly string[] RowLabels;
        private static readonly string[] ColumnLabels;
        private static readonly string[] ColumnLabelsL;

        public static string Id(this IWellLoc well)
        {
            return string.Format("{0}_{1}", well.SamplePlateName, well.LongPlateCoords());
        }


        public static string ShortPlateCoords(this IWellLoc wellLoc)
        {
            if ((wellLoc.Row.HasValue) && (wellLoc.Column.HasValue))
            {
                return RowLabels[wellLoc.Row.Value] + ColumnLabels[wellLoc.Column.Value];
            }
            return "Unassigned";
        }

        public static string LongPlateCoords(this IWellLoc wellLoc)
        {
            try
            {
                if ((wellLoc.Row.HasValue) && (wellLoc.Column.HasValue))
                {
                    return RowLabels[wellLoc.Row.Value] + ColumnLabelsL[wellLoc.Column.Value];
                }
                return "Unassigned";
            }
            catch
            {
                return string.Format("[row:{0}, col:{1}]", wellLoc.Row, wellLoc.Column);
            }
        }

        public static int WellColumn(this string wellLabel)
        {
            try
            {
                return Int32.Parse(wellLabel.Substring(1)) - 1;
            }
            catch (Exception)
            {
                throw new ArgumentException(String.Format("{0} is not a valid well label", wellLabel));
            }
        }

        public static string RowLabel(this int rowIndex)
        {
            if((rowIndex < 0) || (rowIndex > RowLabels.Length))
            {
                throw new ArgumentException("index out of range");
            }
            return RowLabels[rowIndex];
        }

        public static int WellRow(this string wellLabel)
        {
            var pc = wellLabel.Substring(0, 1);

            for (var i = 0; i < 16; i++)
            {
                if (RowLabels[i] == pc) return i;
            }
            throw new ArgumentException(String.Format("{0} is not a valid well label", wellLabel));
        }

        //public static WellLoc ParseWell(this MessageLog log, object data, string plateName, 
        //                                        SamplePlateSize samplePlateSize, string info)
        //{
        //    try
        //    {
        //        if (String.IsNullOrEmpty(data as string))
        //        {
        //            log.AddWithName(String.Format("Well location was blank: {0}", info));
        //            return WellLoc.Empty;
        //        }

        //        return new WellLoc(data as string, plateName, samplePlateSize);
        //    }
        //    catch (Exception)
        //    {
        //        log.AddWithName(String.Format("{0}: Can't parse {1} to a well", info, data));
        //        return WellLoc.Empty;
        //    }
        //}

        public static Well ToWell(this VialPosition vialPosition, ISamplePlate samplePlate)
        {
            if (samplePlate.SamplePlateSize == SamplePlateSize.Size384)
            {
                var col = (vialPosition.WellIndex - 1) % 24;
                var row = ((vialPosition.WellIndex - 1) - col)/24;
                return new Well(RowLabels[row] + ColumnLabels[col]) { SamplePlate = samplePlate };
            }
            else //SamplePlateSize.Size96
            {
                var col = (vialPosition.WellIndex - 1) % 12;
                var row = ((vialPosition.WellIndex - 1) - col) / 12;
                return new Well(RowLabels[row] + ColumnLabels[col]) {SamplePlate = samplePlate};
            }
        }

        public static int ToVialPositionIndex(this IWellLoc wellLoc)
        {
            if ((wellLoc.Column == null) || (wellLoc.Row == null))
            {
                throw new ArgumentException("Well is not valid");
            }

            if (wellLoc.SamplePlateSize == SamplePlateSize.Size384)
            {
                return wellLoc.Row.Value * 24 + wellLoc.Column.Value + 1;
            }
            //SamplePlateSize.Size96
            return wellLoc.Row.Value * 12 + wellLoc.Column.Value + 1;
        }

        public static int ToEasternIndex(this IWellLoc wellLoc)
        {
            if ((wellLoc.Column == null) || (wellLoc.Row == null))
            {
                throw new ArgumentException("Well is not valid");
            }

            if (wellLoc.SamplePlateSize == SamplePlateSize.Size384)
            {
                return wellLoc.Column.Value * 16 + wellLoc.Row.Value + 1;
            }
            //SamplePlateSize.Size96
            return wellLoc.Column.Value * 8 + wellLoc.Row.Value + 1;
        }

        public static IEnumerable<IWell> In2By2BlockOrder(this ISamplePlate samplePlate)
        {
            for (var dRow = 0; dRow < samplePlate.SamplePlateSize.RowCount(); dRow += 2)
            {
                for (var dCol = 0; dCol < samplePlate.SamplePlateSize.ColumnCount(); dCol += 2)
                {
                    yield return samplePlate.Well(0 + dRow, 0 + dCol);
                    yield return samplePlate.Well(0 + dRow, 1 + dCol);
                    yield return samplePlate.Well(1 + dRow, 0 + dCol);
                    yield return samplePlate.Well(1 + dRow, 1 + dCol);
                }
            }
        }

        public static IEnumerable<IWell[]> By2Blocks(this ISamplePlate samplePlate)
        {
            for (var dRow = 0; dRow < samplePlate.SamplePlateSize.RowCount(); dRow += 2)
            {
                for (var dCol = 0; dCol < samplePlate.SamplePlateSize.ColumnCount(); dCol += 2)
                {
                    yield return new[]
                               {
                                    samplePlate.Well(0 + dRow, 0 + dCol),
                                    samplePlate.Well(0 + dRow, 1 + dCol),
                                    samplePlate.Well(1 + dRow, 0 + dCol),
                                    samplePlate.Well(1 + dRow, 1 + dCol)
                               };
                }
            }
        }

        public static IEnumerable<IWell[]> GinasBlocks(this ISamplePlate samplePlate)
        {
            //Sample 1
            yield return GinaBlockLeft(samplePlate, 0, 0);
            //Sample 2
            yield return GinaBlockLeft(samplePlate, 2, 0);
            //Sample 3
            yield return GinaBlockLeft(samplePlate, 4, 0);
            //Sample 4
            yield return GinaBlockLeft(samplePlate, 6, 0);
            //Sample 5
            yield return GinaBlockLeft(samplePlate, 8, 0);
            //Sample 6
            yield return GinaBlockLeft(samplePlate, 10, 0);
            //Sample 7
            yield return GinaBlockLeft(samplePlate, 12, 0);
            //Sample 8
            yield return GinaBlockLeft(samplePlate, 14, 0);
            //Sample 9
            yield return GinaBlockRight(samplePlate, 0, 8);
            //Sample 10
            yield return GinaBlockRight(samplePlate, 2, 8);
            //Sample 11
            yield return GinaBlockRight(samplePlate, 4, 8);
            //Sample 12
            yield return GinaBlockRight(samplePlate, 6, 8);
            //Sample 13
            yield return GinaBlockRight(samplePlate, 8, 8);
            //Sample 14
            yield return GinaBlockRight(samplePlate, 10, 8);
            //Sample 15
            yield return GinaBlockRight(samplePlate, 12, 8);
            //Sample 16
            yield return GinaBlockRight(samplePlate, 14, 8);
            //Sample 17
            yield return GinaBlockLeft(samplePlate, 0, 1);
            //Sample 18
            yield return GinaBlockLeft(samplePlate, 2, 1);
            //Sample 19
            yield return GinaBlockLeft(samplePlate, 4, 1);
            //Sample 20
            yield return GinaBlockLeft(samplePlate, 6, 1);
            //Sample 21
            yield return GinaBlockLeft(samplePlate, 8, 1);
            //Sample 22
            yield return GinaBlockLeft(samplePlate, 10, 1);
            //Sample 23
            yield return GinaBlockLeft(samplePlate, 12, 1);
            //Sample 24
            yield return GinaBlockLeft(samplePlate, 14, 1);
            //Sample 25
            yield return GinaBlockRight(samplePlate, 0, 9);
            //Sample 26
            yield return GinaBlockRight(samplePlate, 2, 9);
            //Sample 27
            yield return GinaBlockRight(samplePlate, 4, 9);
            //Sample 28
            yield return GinaBlockRight(samplePlate, 6, 9);
            //Sample 29
            yield return GinaBlockRight(samplePlate, 8, 9);
            //Sample 30
            yield return GinaBlockRight(samplePlate, 10, 9);
            //Sample 31
            yield return GinaBlockRight(samplePlate, 12, 9);
            //Sample 32
            yield return GinaBlockRight(samplePlate, 14, 9);
        }


        public static IWell[] GinaBlockLeft(ISamplePlate samplePlate, int firstRow, int firstCol)
        {
            return new[]
                        {
                            samplePlate.Well(firstRow + 0, firstCol + 0),
                            samplePlate.Well(firstRow + 0, firstCol + 2),
                            samplePlate.Well(firstRow + 0, firstCol + 4),
                            samplePlate.Well(firstRow + 0, firstCol + 6),
                            samplePlate.Well(firstRow + 1, firstCol + 0),
                            samplePlate.Well(firstRow + 1, firstCol + 2),
                            samplePlate.Well(firstRow + 1, firstCol + 4),
                            samplePlate.Well(firstRow + 1, firstCol + 6),
                            samplePlate.Well(firstRow + 1, firstCol + 8),
                            samplePlate.Well(firstRow + 1, firstCol + 10)
                        };
        }

        public static IWell[] GinaBlockRight(ISamplePlate samplePlate, int firstRow, int firstCol)
        {
            return new[]
                        {
                            samplePlate.Well(firstRow + 0, firstCol + 0),
                            samplePlate.Well(firstRow + 0, firstCol + 2),
                            samplePlate.Well(firstRow + 0, firstCol + 4),
                            samplePlate.Well(firstRow + 0, firstCol + 6),
                            samplePlate.Well(firstRow + 1, firstCol + 4),
                            samplePlate.Well(firstRow + 1, firstCol + 6),
                            samplePlate.Well(firstRow + 1, firstCol + 8),
                            samplePlate.Well(firstRow + 1, firstCol + 10),
                            samplePlate.Well(firstRow + 1, firstCol + 12),
                            samplePlate.Well(firstRow + 1, firstCol + 14)
                        };
        }

        public static IEnumerable<IWellLoc> InWesternReadingOrder(ISamplePlate samplePlate)
        {
            for (var dRow = 0; dRow < samplePlate.SamplePlateSize.RowCount(); dRow ++)
            {
                for (var dCol = 0; dCol < samplePlate.SamplePlateSize.ColumnCount(); dCol ++)
                {
                    yield return samplePlate.Well(dRow, dCol);
                }
            }
        }

        public static IEnumerable<IWellLoc> InEasternReadingOrder(ISamplePlate samplePlate)
        {
            for (var dCol = 0; dCol < samplePlate.SamplePlateSize.ColumnCount(); dCol++)
            {
                for (var dRow = 0; dRow < samplePlate.SamplePlateSize.RowCount(); dRow++)
                {
                    yield return samplePlate.Well(dRow, dCol);
                }
            }
        }

        public static Point GetSize(this SamplePlateSize samplePlateSize)
        {
            if(samplePlateSize == SamplePlateSize.Size96)
            {
                return new Point { X = 12, Y = 8 };
            }
            return new Point { X = 24, Y = 16 };
        }

        //public static IEnumerable<Well> SamplePlate96InReadingOrder()
        //{
        //    get
        //    {
        //        for(var i = 0; i < 8; i++)
        //        {
        //            for (var j = 0; j < 12; j++)
        //            {
        //                yield return new Well(i, j);
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// Groups plate wells as {A1, A2, ..A6}, {A7, A8, ..A12}, {B1, B2, ..B6}, {B7, B8, ..B12}, ...
        ///// </summary>
        //public static IEnumerable<IEnumerable<Well>> SamplePlateHalfRows96
        //{
        //    get
        //    {
        //        var retList = new List<Well>();
        //        foreach (var well in SamplePlate96InReadingOrder)
        //        {
        //            if(retList.Count == 6)
        //            {
        //                yield return retList;
        //                retList = new List<Well>();
        //            }
        //            retList.Add(well);
        //        }
        //        yield return retList;
        //    }
        //}

        //public static bool WellsHaveNoGapsInReadingOrder(IEnumerable<IWell> wells)
        //{
        //    var wellsInReadingOrder = SamplePlate96InReadingOrder.ToList();
        //    var wellList = wells.OrderBy(T => T, new WellSorterReadingOrder());
            
        //    int index = 0;
        //    foreach (var well in wellList)
        //    {
        //       if( (well.Row != wellsInReadingOrder[index].Row) || (well.Column != wellsInReadingOrder[index].Column) )
        //       {
        //           return false;
        //       }
        //        index++;
        //    }
        //    return true;
        //}
    }
}
