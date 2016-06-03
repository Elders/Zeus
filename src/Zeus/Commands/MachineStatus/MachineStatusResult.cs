using CommandLine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Zeus.Linux.Cli.Monitoring;

namespace Zeus.Linux.Cli.Commands
{
    public class MachineStatusResult
        {
            private MachineStatusResult() { }

            public MachineStatusResult(SystemStatus status)
            {
                Cpu = new CpuResult(status.CpuInfo);
                Drives = status.DrivesInfo.ToDictionary(x => x.VolumeName, x => new DriveResult(x));
                Ram = new MemoryResult(status.MemoryInfo);
            }

            public CpuResult Cpu { get; set; }

            public Dictionary<string, DriveResult> Drives { get; set; }

            public MemoryResult Ram { get; set; }

            public bool Success { get { return Error == false; } }

            public bool Error { get { return Errors != null && Errors.Count > 0; } }

            public List<string> Errors { get; set; }

            public void AddErrors(params string[] errors)
            {
                if (Errors == null)
                    Errors = new List<string>();
                Errors.AddRange(errors);
            }

            public static MachineStatusResult Failure(Exception ex)
            {
                var result = new MachineStatusResult();
                result.AddErrors(ex.Message, ex.StackTrace);
                return result;
            }
        }
}