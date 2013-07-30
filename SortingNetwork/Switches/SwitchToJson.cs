using SortingNetwork.KeyPair;

namespace SortingNetwork.Switches
{
    public class SwitchToJson
    {
        public static SwitchToJson Make(ISwitch @switch)
        {
            return new SwitchToJson
            {
                V = @switch.ToShortString()
            };
        }

        public string V { get; set; }
    }

    public static class SwitchToJsonEx
    {
        public static ISwitch ToSwitch(this string shortString, int keyCount)
        {
            var pcs = shortString.Split(" ".ToCharArray());
            return Switch.Make
                (
                    int.Parse(pcs[0]),
                    KeySet.Instance.GetKeyPair(int.Parse(pcs[1]), int.Parse(pcs[2]), keyCount)
                );
        }

        public static string ToShortString(this ISwitch @switch)
        {
            return string.Format("{0} {1} {2}", @switch.Index, @switch.KeyPair.LowKey, @switch.KeyPair.HiKey);
        }
    }

}
