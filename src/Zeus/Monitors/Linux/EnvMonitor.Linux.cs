using Zeus.Monitors.Linux.Commands;

namespace Zeus.Monitors.Linux
{
    public class EnvMonitor : IEnvMonitor
    {
        public EnvInfo GetEnvInfo()
        {
            var osName = LinuxCommand.Execute("cat", "/etc/issue");

            return new EnvInfo(osName.Split('\\')[0]);
        }
    }
}
