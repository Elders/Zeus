namespace Zeus.Linux.Cli
{
    public interface ICpuMonitor
    {
        CpuInfo GetCpuUsage();
    }
    public class CpuInfo
    {
        public CpuInfo(double usageInPercent)
        {
            UsageInPercent = usageInPercent;
        }

        public double UsageInPercent { get; private set; }
    }
}