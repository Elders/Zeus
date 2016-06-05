using System;
using System.Management;
using System.Linq;

namespace Zeus.Monitors.Windows
{
    public class WindowsRamMontior : IMemoryMonitor
    {
        const int BytesInMB = 1024;

        public MemoryInfo GetMemoryUsage()
        {
            return new MemoryInfo(GetMemoryTotal(), GetMemoryFree());
        }

        private decimal GetMemoryTotal()
        {
            var select = new SelectQuery("Win32_OPeratingSystem");
            var searcher = new ManagementObjectSearcher(select);

            ManagementObjectCollection collection = searcher.Get();
            ManagementObject queryObj = collection.Cast<ManagementObject>().First();

            return Convert.ToInt32(queryObj["TotalVisibleMemorySize"]) / 1024;
        }

        private decimal GetMemoryFree()
        {
            var select = new SelectQuery("Win32_OPeratingSystem");
            var searcher = new ManagementObjectSearcher(select);

            ManagementObjectCollection collection = searcher.Get();
            ManagementObject queryObj = collection.Cast<ManagementObject>().First();

            return Convert.ToInt32(queryObj["FreePhysicalMemory"]) / 1024;
        }
    }
}