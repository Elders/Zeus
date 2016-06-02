using CommandLine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Zeus.Linux.Cli.Monitoring;

namespace Zeus.Linux.Cli.Commands
{
    public class DSLCpu
    {
        public DSLCpu(CpuInfo info)
        {
            usage = info.UsageInPercent;
        }

        public decimal usage { get; set; }
    }
}