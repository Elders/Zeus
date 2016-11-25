using Zeus.Monitors;

namespace Zeus.Commands.ProcessStatus
{
    public class ProcessCpuResult
    {
        public ProcessCpuResult(ProcessCpuInfo info)
        {
            Usage = info.UsageInPercent;
        }

        public decimal Usage { get; set; }
    }
}
