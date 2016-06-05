using System;
using System.Collections.Generic;
using System.Linq;
using Zeus.Monitors;

namespace Zeus.Commands.MachineStatus
{
    public class MachineStatusResult
    {
        private MachineStatusResult() { }

        public MachineStatusResult(SystemStatus status)
        {
            Cpu = new CpuResult(status.CpuInfo);
            Drives = status.DrivesInfo.ToDictionary(x => x.VolumeName.Replace(":", string.Empty), x => new DriveResult(x));
            if (Drives.ContainsKey("total") == false && status.DrivesInfo.Any())
            {
                var totalSpace = status.DrivesInfo.Sum(x => x.TotalSpace);
                var freeSpace = status.DrivesInfo.Sum(x => x.FreeSpace);
                var total = new DriveInfo("total", totalSpace, freeSpace);
                Drives.Add(total.VolumeName, new DriveResult(total));
            }
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