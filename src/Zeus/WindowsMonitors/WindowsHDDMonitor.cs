using System;
using System.Collections.Generic;

namespace Zeus.Linux.Cli
{
    public class WindowsHDDMonitor : IHDDMonitor
    {
        const int BytesInMB = 1024;

        public IEnumerable<DriveInfo> GetHDDUsage()
        {
            SelectQuery query = new SelectQuery("Select * from Win32_LogicalDisk");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection queryCollection = searcher.Get();

            HashSet<DriveInfo> disksInfo = new HashSet<DriveInfo>();

            foreach (ManagementObject mo in queryCollection)
            {
                var total = Convert.ToInt64(mo["Size"]) / 1042 / 1024;
                var free = Convert.ToInt64(mo["FreeSpace"]) / 1042 / 1024;
                var volume = mo["Name"].ToString();

                disksInfo.Add(new DriveInfo(volume, total, free));
            }

            yield return disksInfo;
        }
    }
}