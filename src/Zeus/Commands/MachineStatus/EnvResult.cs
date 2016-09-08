using Zeus.Monitors;

namespace Zeus.Commands.MachineStatus
{
    public class EnvResult
    {
        public EnvResult(EnvInfo env)
        {
            OS = env.OSName;
            TimeInfo = env.Time;
        }

        public string OS { get; set; }

        public EnvTimeInfo TimeInfo { get; set; }
    }
}
