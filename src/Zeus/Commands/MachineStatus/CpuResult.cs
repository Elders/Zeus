using Zeus.Monitors;

namespace Zeus.Commands.MachineStatus
{
    public class CpuResult
    {
        public CpuResult(CpuInfo info)
        {
            Usage = info.UsageInPercent;
        }

        public decimal Usage { get; set; }
    }
}