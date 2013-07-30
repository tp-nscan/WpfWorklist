namespace Lab.Model
{
    public enum ApicalOrBasal
    {
        Apical,
        Basal,
        Unknown
    }

    public enum DonorOrAcceptor
    {
        Donor,
        Acceptor
    }

    public enum StandardOrExperimental
    {
        Blank,
        Experimental,
        Standard
    }

    public enum CompoundOrControl
    {
        Cmpd,
        Control
    }

    //public static class AdmeTermsExt
    //{
    //    public static ApicalOrBasal ParseApicalOrBasal(this MessageLog log, string data, string info)
    //    {
    //        try
    //        {
    //            if (string.IsNullOrEmpty(data))
    //            {
    //                return ApicalOrBasal.Unknown;
    //            }
    //            return (ApicalOrBasal)Enum.Parse(typeof(ApicalOrBasal), data);
    //        }
    //        catch (Exception)
    //        {
    //            log.AddWithName(String.Format("{0}: Can't parse {1} to a well", info, data));
    //            return ApicalOrBasal.Unknown;
    //        }
    //    }
    //}
}
