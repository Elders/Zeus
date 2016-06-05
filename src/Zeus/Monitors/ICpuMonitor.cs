namespace Zeus.Monitors
{
    public interface ICpuMonitor
    {
        CpuInfo GetCpuUsage();
    }
    public class CpuInfo
    {
        public CpuInfo(decimal usageInPercent)
        {
            UsageInPercent = usageInPercent;
        }

        public decimal UsageInPercent { get; private set; }
    }
}