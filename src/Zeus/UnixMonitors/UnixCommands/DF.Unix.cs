using System.Collections.Generic;
using System.Linq;

namespace Zeus.Linux.Cli
{
    public static class UnixDF
    {
        public static Dictionary<string, string> Execute()
        {
            var result = new Dictionary<string, string>();

            var dfResult = UnixCommand.Execute("df", "-h --total").Split('\n').FirstOrDefault(str => str.Contains("total")).Split(new char[] { ' ' }).Where(x => !string.IsNullOrEmpty(x)).ToArray();

            var driveTotal = dfResult[1].Replace("G", "");
            var driveUsed = dfResult[2].Replace("G", "");
            var driveFree = dfResult[3].Replace("G", "");

            result.Add("drive_total", driveTotal);
            result.Add("drive_used", driveUsed);
            result.Add("drive_free", driveFree);


            return result;
        }
    }
}