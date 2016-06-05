namespace Zeus.Monitors
{
    public interface IMemoryMonitor
    {
        MemoryInfo GetMemoryUsage();
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

        public decimal UsedMemoryPercent { get { return ((TotalMemory - FreeMemory) / TotalMemory) * 100; } }
    }
}