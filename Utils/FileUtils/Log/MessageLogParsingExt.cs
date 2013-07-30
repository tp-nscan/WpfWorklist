using System;
using Utils.FileUtils.Log;

namespace Utils
{
    public static class MessageLogParsingExt
    {
        public static double ParseDouble(this MessageLog log, string data, double defaultValue, string info)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    return defaultValue;
                }
                return double.Parse(data);
            }
            catch (Exception)
            {
                log.AddWithName(String.Format("{0}: Can't parse {1} to double", info, data));
                return defaultValue;
            }
        }

        public static int ParseInt(this MessageLog log, string data, int defaultValue, string info)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    return defaultValue;
                }
                return int.Parse(data);
            }
            catch (Exception)
            {
                log.AddWithName(String.Format("{0}: Can't parse {1} to int", info, data));
                return defaultValue;
            }
        }

        public static string ParseString(this MessageLog log, string data, string defaultValue, string info)
        {
            if (string.IsNullOrEmpty(data))
            {
                return defaultValue;
            }
            return data;
        }

    }
}
