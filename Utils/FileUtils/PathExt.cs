using System;
using System.IO;

namespace Utils.FileUtils
{
    public static class PathExt
    {
        private static readonly string[] monthLabels;

        static PathExt()
        {
            monthLabels = new[] {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
        }

        public static string FileStringA(this DateTime dateTime)
        {
            return String.Format("_{0}d{1}m{2}y", dateTime.Day, dateTime.Month, dateTime.Year);
        }

        public static string FileStringB(this DateTime dateTime)
        {
            return String.Format("{0}{1}{2}", dateTime.Year, monthLabels[dateTime.Month], dateTime.Day);
        }

        public static string LastBranchOnPath(this string path)
        {
            var pcs = path.Split(Path.DirectorySeparatorChar);
            return pcs[pcs.Length - 1];
        }

        public static string LocalName(this System.Type type)
        {
            var pcs = type.FullName.Split(".".ToCharArray());
            return pcs[pcs.Length - 1];
        }

        public static string GetFileNameWithSuffix(string filePath, string suffix, string newExt = null)
        {
            var ext = newExt ?? Path.GetExtension(filePath);
            var fileName = Path.GetFileNameWithoutExtension(filePath);

            return  fileName +
                    suffix +
                    "." +
                    ext;
        }

        public static string GetFileNameWithReplacement(string filePath, string partToReplace, string replacement,
            string newExt = null)
        {
            var ext = newExt ?? Path.GetExtension(filePath);
            var fileName = Path.GetFileNameWithoutExtension(filePath);

            if (fileName == null)
            {
                return string.Empty;
            }

            return  fileName.Replace(partToReplace, replacement) +
                    "." +
                    ext;
        }
    }
}
