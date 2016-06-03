using CommandLine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Zeus.Linux.Cli.Monitoring;

namespace Zeus.Linux.Cli.Commands
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