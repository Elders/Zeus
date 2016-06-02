namespace Zeus.Linux.Cli
{
    public class WindowsCpuMonitor : ICpuMonitor
    {
        public CpuInfo GetCpuUsage()
        {
            return new CpuInfo(50);
        }
    }
}