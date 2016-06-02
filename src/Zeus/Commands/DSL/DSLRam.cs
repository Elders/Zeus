using CommandLine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Zeus.Linux.Cli.Monitoring;

namespace Zeus.Linux.Cli.Commands
{
    public class DSLRam
    {
        public DSLRam(MemoryInfo info)
        {
            free = info.FreeMemory;
            occupied = info.TotalMemory - free;
            total = info.TotalMemory;
            usage = info.UsedMemoryPercent;
        }
        public decimal free { get; set; }
        public decimal total { get; set; }
        public decimal occupied { get; set; }
        public decimal usage { get; set; }
    }
}