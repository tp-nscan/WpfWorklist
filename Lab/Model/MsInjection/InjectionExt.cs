using System;
using Lab.Model.Containers;
using Lab.Model.Containers.Plate;
using Lab.Model.Containers.Single;

namespace Lab.Model.MsInjection
{
    public static class InjectionExt
    {
        public static string LongWellCoords(this IInjection injection)
        {
            switch (injection.Source.ContainerType)
            {
                case ContainerType.Well:
                    var well = injection.Source as IWellLoc;
                    if (well == null)
                    {
                        return "no source";
                    }
                    if (well.SamplePlateName == null)
                    {
                        return "no plate for source";
                    }
                    return well.LongPlateCoords();
                //case ContainerType.Empty:
                //case ContainerType.Bottle:
                default:
                    return string.Empty;
            }
        }

        public static string ShortWellCoords(this IInjection injection)
        {
            switch (injection.Source.ContainerType)
            {
                case ContainerType.Well:
                    var well = injection.Source as IWellLoc;
                    if (well == null)
                    {
                        return "no source";
                    }
                    if (well.SamplePlateName == null)
                    {
                        return "no plate for source";
                    }
                    return well.ShortPlateCoords();
                //case ContainerType.Empty:
                //case ContainerType.Bottle:
                default:
                    return string.Empty;
            }
        }

        public static IWellLoc WellLoc(this IInjection injection)
        {
            switch (injection.InjectionType)
            {
                case InjectionType.Well:
                    return injection.Source as IWellLoc;
                //case ContainerType.Empty:
                //case ContainerType.Bottle:
                default:
                    return null;
            }
        }

        //public static IInjection ParseInjection(this MessageLog log, string data, string info, SamplePlateGroup samplePlateGroup)
        //{
        //    try
        //    {
        //        if (String.IsNullOrEmpty(data))
        //        {
        //            log.AddWithName(String.Format("Injection was blank: {0}", info));
        //            return new UnknownInjection();
        //        }
        //        if (data == UnknownInjection.UnknownInjectionId)
        //        {
        //            return new UnknownInjection();
        //        }

        //        return data.ParseInjection(samplePlateGroup);
        //    }
        //    catch (Exception)
        //    {
        //        log.AddWithName(String.Format("{0}: Can't parse {1} to a Injection", info, data));
        //        return new UnknownInjection();
        //    }
        //}

        public static IInjection ParseInjection(this string descr, SamplePlateGroup samplePlateGroup)
        {
            var pcs = descr.Split("_".ToCharArray());
            switch (pcs[0])
            {
                case "b":
                    return new BottleInjection(
                        new BottleLoc(pcs[1]), int.Parse(pcs[3]), 
                        samplePlateGroup.GetOrMakeSamplePlate(pcs[2]));
                case "B":
                    return new BottleInjection(new BottleLoc(pcs[1]), int.Parse(pcs[3]),
                        samplePlateGroup.GetOrMakeSamplePlate(pcs[2]));
                case "w":
                    return new WellInjection(
                        new WellLoc(pcs[2], pcs[1], SamplePlateSize.Size96), int.Parse(pcs[3]),
                        samplePlateGroup.GetOrMakeSamplePlate(pcs[1]));
                case "W":
                    return new WellInjection(
                        new WellLoc(pcs[2], pcs[1], SamplePlateSize.Size384), int.Parse(pcs[3]),
                        samplePlateGroup.GetOrMakeSamplePlate(pcs[1]));
                //case "e":
                default:
                    throw new Exception(String.Format("Injection type {0} not handled in ParseInjection", descr));
            }
        }

    }
}
