using System.Collections.Generic;
using System.Linq;

namespace Zeus.Monitors.Linux.Commands
{
    public static class DF
    {
        public static IEnumerable<Dictionary<string, string>> Execute()
        {
            var dfResultRows = LinuxCommand.Execute("df", "-T").Split('\n');
            var result = new List<Dictionary<string, string>>();

            for (int row = 1; row < dfResultRows.Length; row++)
            {
                var itemToArray = dfResultRows[row].Split(new char[] { ' ' }).Where(x => !string.IsNullOrEmpty(x)).ToArray();

                if (itemToArray.Length >= 2)
                {
                    if (itemToArray[1].Contains("ext"))
                    {
                        var driveInfo = new Dictionary<string, string>();

                        var driveName = itemToArray[1].Replace("G", "");
                        var driveUsed = itemToArray[3].Replace("G", "");
                        var driveFree = itemToArray[4].Replace("G", "");
                        var driveTotal = ulong.Parse(driveUsed) + ulong.Parse(driveFree);

                        driveInfo.Add("drive_name", driveName);
                        driveInfo.Add("drive_free", driveFree);
                        driveInfo.Add("drive_used", driveUsed);
                        driveInfo.Add("drive_total", driveTotal.ToString());

                        result.Add(driveInfo);
                    }
                }
            }

            return result;
        }
    }
}