using SortNetwork.KeySets;
using SortNetwork.Results;

namespace SortNetwork.Json.Results
{
    public class SwitchResultToJson
    {
        public static SwitchResultToJson Make(ISwitchResult @switch)
        {
            return new SwitchResultToJson
            {
                V = @switch.ToShortString()
            };
        }

        public string V { get; set; }

    }

    public static class SwitchResultToJsonEx
    {
        public static ISwitchResult ToSwitchResult(this string shortString, int keyCount)
        {
            var pcs = shortString.Split(" ".ToCharArray());
            return SwitchResult.Make
                (
                    int.Parse(pcs[0]),
                    KeySet.Instance.GetKeyPair(int.Parse(pcs[1]), int.Parse(pcs[2]), keyCount),
                    int.Parse(pcs[3])
                );
        }

        public static string ToShortString(this ISwitchResult @switch)
        {
            return string.Format
                (
                    "{0} {1} {2} {3}", 
                    @switch.Index, 
                    @switch.KeyPair.LowKey,
                    @switch.KeyPair.HiKey,
                    @switch.UseCount
                );
        }
    }
}
