using System;

namespace Zeus.Linux.Cli
{
    public class WindowsCpuMonitor : ICpuMonitor
    {
        public CpuInfo GetCpuUsage()
        {
            var select = new SelectQuery("Win32_PerfFormattedData_PerfOS_Processor");
            var searcher = new ManagementObjectSearcher(select);

            ManagementObjectCollection collection = searcher.Get();
            ManagementObject queryObj = collection.Cast<ManagementObject>().First();

            return new CpuInfo(100 - Convert.ToInt32(queryObj["PercentIdleTime"]));
        }
    }
}