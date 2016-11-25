using System.Collections.Generic;

namespace Zeus.Monitors
{
    public interface IMemoryMonitor
    {
        MemoryInfo GetMemoryUsage();
    }
    public interface IProcessMemoryMonitor
    {
        IEnumerable<ProcessMemoryInfo> GetMemoryUsage(string processName);
    }

    public class MemoryInfo
    {
        public MemoryInfo(decimal totalMemory, decimal freeMemory)
        {
            TotalMemory = totalMemory;
            FreeMemory = freeMemory;
        }

        public decimal TotalMemory { get; private set; }

        public decimal FreeMemory { get; private set; }

        public decimal UsedMemoryPercent { get { return (TotalMemory == 0) ? 0 : ((TotalMemory - FreeMemory) / TotalMemory) * 100; } }
    }

    public class ProcessMemoryInfo
    {
        public ProcessMemoryInfo(int pid, decimal totalProcessMem, decimal totalMachineMem)
        {
            PID = pid;
            TotalProcessUsage = totalProcessMem;
            TotalMachineMem = totalMachineMem;
        }

        public int PID { get; set; }
        public decimal TotalProcessUsage { get; set; }
        public decimal TotalMachineMem { get; set; }

        public decimal UsedMemoryPercent { get { return (TotalProcessUsage == 0) ? 0 : (TotalProcessUsage / TotalMachineMem) * 100; } }
    }
}