using System.Collections.Generic;
using Zeus.Facility;

namespace Zeus.Monitors
{
    public class ProcessMonitor
    {
        private IMonitorFacility facility;

        public ProcessMonitor(IMonitorFacility facility)
        {
            this.facility = facility;
        }

        public ProcessStatus GetProcessStatus(string processName)
        {
            var cpuMonitor = facility.Resolve<IProcessCpuMonitor>();
            var memoryInfo = facility.Resolve<IProcessMemoryMonitor>();

            return new ProcessStatus(processName, cpuMonitor.GetCpuUsage(processName), memoryInfo.GetMemoryUsage(processName));
        }
    }

    public class ProcessStatus
    {
        public ProcessStatus(string processName, IEnumerable<ProcessCpuInfo> cpuInfo, IEnumerable<ProcessMemoryInfo> memoryInfo)
        {
            Name = processName;
            CpuInfo = cpuInfo;
            MemoryInfo = memoryInfo;
        }

        public string Name { get; set; }

        public IEnumerable<ProcessCpuInfo> CpuInfo { get; set; }

        public IEnumerable<ProcessMemoryInfo> MemoryInfo { get; set; }
    }
}
