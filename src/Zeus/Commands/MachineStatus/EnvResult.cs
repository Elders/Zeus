using Zeus.Monitors;

namespace Zeus.Commands.MachineStatus
{
    public class EnvResult
    {
        public EnvResult(EnvInfo env)
        {
            OS = env.OSName;
            Time = env.Time.Time;
            TimeZone = env.Time.TimeZone;
            UtcOffset = env.Time.UtcOffset;
        }

        public string OS { get; set; }

        public string Time { get; set; }

        public string TimeZone { get; set; }

        public string UtcOffset { get; set; }
    }
}
