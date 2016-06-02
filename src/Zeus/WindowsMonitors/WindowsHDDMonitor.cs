using System.Collections.Generic;

namespace Zeus.Linux.Cli
{
    public class WindowsHDDMonitor : IHDDMonitor
    {
        const int BytesInMB = 1024;

        public IEnumerable<DriveInfo> GetHDDUsage()
        {
            yield return new DriveInfo("total", 80, 40);
        }
    }
}