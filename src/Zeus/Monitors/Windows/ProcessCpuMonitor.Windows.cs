using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Zeus.Monitors.Windows
{
    public class WindowsProcessCpuMonitor : IProcessCpuMonitor
    {
        static string[] systemProcess = { "services", "Memory Compression", "csrss", "wininit", "NisSrv", "smss", "MsMpEng", "System", "Idle", "svchost" };

        public IEnumerable<ProcessCpuInfo> GetCpuUsage(string moduleName)
        {
            float cpuUseage = 0;
            IList<ProcessCpuInfo> result = new List<ProcessCpuInfo>();


            Process[] runningNow = Process.GetProcesses();
            foreach (Process process in runningNow)
            {
                if (!systemProcess.Contains(process.ProcessName) && process.MainModule.ModuleName == moduleName)
                {
                    using (PerformanceCounter pcProcess = new PerformanceCounter("Process", "% Processor Time", process.ProcessName))
                    {
                        pcProcess.NextValue();

                        cpuUseage = pcProcess.NextValue() / Environment.ProcessorCount;
                        result.Add(new ProcessCpuInfo(process.Id, (decimal)cpuUseage));
                    }
                }
            }



            return result;
        }
    }
}
