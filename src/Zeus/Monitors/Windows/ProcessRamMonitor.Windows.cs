using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading;

namespace Zeus.Monitors.Windows
{


    public class WindowsProcessRamMontior : IProcessMemoryMonitor
    {
        static string[] systemProcess = { "services", "Memory Compression", "csrss", "wininit", "NisSrv", "smss", "MsMpEng", "System", "Idle" };

        public IEnumerable<ProcessMemoryInfo> GetMemoryUsage(string moduleName)
        {
            decimal memUseage = 0;
            decimal totalMem = GetMachineTotalMemory();
            IList<ProcessMemoryInfo> result = new List<ProcessMemoryInfo>();

            Process[] runningNow = Process.GetProcesses();
            foreach (Process process in runningNow)
            {
                if (!systemProcess.Contains(process.ProcessName) && process.MainModule.ModuleName == moduleName)
                {
                    process.Refresh();
                    memUseage = process.WorkingSet64 / 1024 / 1024;
                    result.Add(new ProcessMemoryInfo(process.Id, (decimal)memUseage, totalMem));

                }
            }

            return result;
        }

        public decimal GetMachineTotalMemory()
        {
            var select = new SelectQuery("Win32_OPeratingSystem");
            var searcher = new ManagementObjectSearcher(select);

            ManagementObjectCollection collection = searcher.Get();
            ManagementObject queryObj = collection.Cast<ManagementObject>().First();

            return Convert.ToInt32(queryObj["TotalVisibleMemorySize"]) / 1024;
        }
    }
}
