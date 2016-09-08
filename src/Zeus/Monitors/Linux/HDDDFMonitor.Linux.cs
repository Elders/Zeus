using System.Collections.Generic;
using Zeus.Monitors.Linux.Commands;

namespace Zeus.Monitors.Linux
{
    public class HDDDFMonitor : IHDDMonitor
    {
        const int BytesInMB = 1024;

        public IEnumerable<DriveInfo> GetHDDUsage()
        {
            var result = new List<DriveInfo>();
            var freeSpace = 0m;
            var total = 0m;

            var drives = DF.Execute();

            foreach (var driveInfo in drives)
            {
                var driveNameAsString = "";
                if (driveInfo.TryGetValue("drive_name", out driveNameAsString)) { }

                var driveTotalAsString = "";
                if (driveInfo.TryGetValue("drive_total", out driveTotalAsString))
                {
                    ulong driveTotalAsDecimal = 0;
                    if (ulong.TryParse(driveTotalAsString, out driveTotalAsDecimal))
                    {
                        total = driveTotalAsDecimal / BytesInMB;// B to MiB
                    }

                }

                var driveFreeAsString = "";
                if (driveInfo.TryGetValue("drive_free", out driveFreeAsString))
                {
                    ulong driveFreeAsDecimal = 0;
                    if (ulong.TryParse(driveFreeAsString, out driveFreeAsDecimal))
                    {
                        freeSpace = driveFreeAsDecimal / BytesInMB; // B to MiB
                    }

                }

                result.Add(new DriveInfo(driveNameAsString, (int)total, (int)freeSpace));

            }

            return result;
        }
    }
}