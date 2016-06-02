using CommandLine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Zeus.Linux.Cli.Monitoring;

namespace Zeus.Linux.Cli.Commands
{
    public class DSLDrive
    {
        public DSLDrive(DriveInfo info)
        {
            free = info.FreeSpace;
            occupied = info.TotalSpace - free;
            total = info.TotalSpace;
            usage = info.UsedSpaceInPercent;
        }
        public decimal free { get; set; }
        public decimal total { get; set; }
        public decimal occupied { get; set; }
        public decimal usage { get; set; }
    }
}