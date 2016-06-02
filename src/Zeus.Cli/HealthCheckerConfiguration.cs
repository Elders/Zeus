namespace Zeus.Linux.Cli
{
    public class HealthCheckerConfiguration
    {
        public HealthCheckerConfiguration()
        {

        }

        public ulong? CriticalPointAvailablePhysicalMemory { get; set; }
        public long? CriticalPointDriveFreeSpace { get; set; }
        public float? CriticalPointCPU { get; set; }
    }
}
