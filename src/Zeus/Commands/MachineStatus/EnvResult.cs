using Zeus.Monitors;

namespace Zeus.Commands.MachineStatus
{
    public class EnvResult
    {
        public EnvResult(EnvInfo env)
        {
            OS = env.OSName;
        }

        public string OS { get; set; }
    }
}
