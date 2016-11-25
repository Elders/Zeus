using Zeus.Monitors;

namespace Zeus.Commands.ProcessStatus
{
    public class ProcessMemoryResult
    {
        public ProcessMemoryResult(ProcessMemoryInfo info)
        {
            Usage = info.TotalProcessUsage;
        }
        public decimal Usage { get; set; }
    }
}
