using System;
using Zeus.Monitors.Linux.Commands;

namespace Zeus.Monitors.Linux
{
    public class EnvMonitor : IEnvMonitor
    {
        const string timeFmt = "{0:yyyy-MM-dd HH:mm:ss}";

        public EnvInfo GetEnvInfo()
        {
            var osName = LinuxCommand.Execute("cat", "/etc/issue");

            var time = DateTime.Now;

            TimeZoneInfo localZone = TimeZoneInfo.Local;
            string zone = localZone.StandardName;
            string utcOffset = localZone.GetUtcOffset(time).TotalHours.ToString();
            var timeInfo = new EnvTimeInfo(String.Format(timeFmt, time), zone, utcOffset);

            return new EnvInfo(osName.Split('\\')[0], timeInfo);
        }
    }
}
