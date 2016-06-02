
namespace Zeus.Linux.Cli
{
    public class WindowsRamMontior : IMemoryMonitor
    {
        const int BytesInMB = 1024;

        public MemoryInfo GetMemoryUsage()
        {
            return new MemoryInfo(16000, 6000);
        }
    }
}