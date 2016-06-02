using System.Collections.Generic;

namespace Zeus.Linux.Cli
{
    public class UnixHDDDFMonitor : IHDDMonitor
    {
        const int BytesInMB = 1024;

        public IEnumerable<DriveInfo> GetHDDUsage()
        {
            var freeSpace = 0m;
            var total = 0m;

            var driveInfo = UnixDF.Execute();

            var driveTotalAsString = "";
            if (driveInfo.TryGetValue("drive_total", out driveTotalAsString))
            {

                decimal driveTotalAsDecimal = 0;
                if (decimal.TryParse(driveTotalAsString, out driveTotalAsDecimal))
                {
                    total = driveTotalAsDecimal * BytesInMB;// GiB to MiB
                }

            }

            var driveFreeAsString = "";
            if (driveInfo.TryGetValue("drive_free", out driveFreeAsString))
            {
                decimal driveFreeAsDecimal = 0;
                if (decimal.TryParse(driveFreeAsString, out driveFreeAsDecimal))
                {
                    freeSpace = driveFreeAsDecimal * BytesInMB; // GiB to MiB
                }

            }

            yield return new DriveInfo("total", (int)total, (int)freeSpace);
        }
    }
}