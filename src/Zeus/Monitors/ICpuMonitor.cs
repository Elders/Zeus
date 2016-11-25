using System.Collections.Generic;

namespace Zeus.Monitors
{
    public interface ICpuMonitor
    {
        CpuInfo GetCpuUsage();
    }

    public interface IProcessCpuMonitor
    {
        IEnumerable<ProcessCpuInfo> GetCpuUsage(string name);
    }

    public class CpuInfo
    {
        public CpuInfo(decimal usageInPercent)
        {
            UsageInPercent = usageInPercent;
        }

        public decimal UsageInPercent { get; private set; }
    }

    public class ProcessCpuInfo
    {
        public ProcessCpuInfo(int pid, decimal usageInPercent)
        {
            PID = pid;
            UsageInPercent = usageInPercent;
        }

        public int PID { get; private set; }

        public decimal UsageInPercent { get; private set; }

    }
}