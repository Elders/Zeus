using System;
using System.Collections.Generic;
using System.Linq;
using Zeus.Commands.MachineStatus;

namespace Zeus.Commands.ProcessStatus
{
    public class ProcessStatusResult
    {

        public ProcessStatusResult() { }

        public ProcessStatusResult(Monitors.ProcessStatus status)
        {
            ProcessName = status.Name;
            Cpu = status.CpuInfo.ToDictionary(x => x.PID, x => new ProcessCpuResult(x));

            Ram = status.MemoryInfo.ToDictionary(x => x.PID, x => new ProcessMemoryResult(x));
        }

        public string ProcessName { get; set; }

        public Dictionary<int, ProcessCpuResult> Cpu { get; set; }

        public Dictionary<int, ProcessMemoryResult> Ram { get; set; }

        public bool Success { get { return Error == false; } }

        public bool Error { get { return Errors != null && Errors.Count > 0; } }

        public List<string> Errors { get; set; }

        public void AddErrors(params string[] errors)
        {
            if (Errors == null)
                Errors = new List<string>();
            Errors.AddRange(errors);
        }

        public static ProcessStatusResult Failure(Exception ex)
        {
            var result = new ProcessStatusResult();
            result.AddErrors(ex.Message, ex.StackTrace);
            return result;
        }
    }


}
