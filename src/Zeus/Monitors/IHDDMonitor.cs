using System.Collections.Generic;

namespace Zeus.Monitors
{
    public interface IHDDMonitor
    {
        IEnumerable<DriveInfo> GetHDDUsage();
    }

    public class DriveInfo
    {
        public DriveInfo(string volumeName, decimal totalSpace, decimal freeSpace)
        {
            VolumeName = volumeName;
            TotalSpace = totalSpace;
            FreeSpace = freeSpace;
        }

        public string VolumeName { get; private set; }

        public decimal TotalSpace { get; private set; }

        public decimal FreeSpace { get; private set; }

        public decimal UsedSpaceInPercent { get { return ((TotalSpace - FreeSpace) / TotalSpace) * 100; } }
    }
}