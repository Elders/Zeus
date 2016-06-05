using Zeus.Monitors;

namespace Zeus.Commands.MachineStatus
{
    public class MemoryResult
    {
        public MemoryResult(MemoryInfo info)
        {
            Free = info.FreeMemory;
            Occupied = info.TotalMemory - Free;
            Total = info.TotalMemory;
            Usage = info.UsedMemoryPercent;
        }
        public decimal Free { get; set; }
        public decimal Total { get; set; }
        public decimal Occupied { get; set; }
        public decimal Usage { get; set; }
    }
}