using System;
using Lab.Model.Containers.Plate;
using Lab.Model.Containers.Single;

namespace Lab.Model.Containers
{
    public static class WorklistSampleExt
    {
        //public static IContainerLoc ParseContainer(this MessageLog log, string data, string info)
        //{
        //    try
        //    {
        //        if (String.IsNullOrEmpty(data))
        //        {
        //            //log.AddWithName(String.Format("Container was blank: {0}", info));
        //            return Container.Empty;
        //        }

        //        if (data == "empty")
        //        {
        //            return Container.Empty;
        //        }

        //        return data.ParseContainer();
        //    }
        //    catch (Exception)
        //    {
        //        //log.AddWithName(String.Format("{0}: Can't parse {1} to a Container", info, data));
        //        return Container.Empty;
        //    }
        //}

        public static IContainerLoc ParseContainer(this string descr)
        {
            var pcs = descr.Split("_".ToCharArray());
            switch (pcs[0])
            {
                case "b":
                case "B":
                    return new BottleLoc( pcs[1]);
                case "w":
                    return new WellLoc(pcs[2], pcs[1], SamplePlateSize.Size96);
                case "W":
                    return new WellLoc(pcs[2], pcs[1], SamplePlateSize.Size384);
                //case "e":
                default:
                    throw new Exception(String.Format("{0}: Container type {1} not handled in ParseContainer", descr));
            }
        }
    }
}   
