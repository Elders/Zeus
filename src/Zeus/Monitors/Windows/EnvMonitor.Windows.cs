using System;
using System.Linq;
using System.Management;

namespace Zeus.Monitors.Windows
{
    public class WindowsEnvMonitor : IEnvMonitor
    {
        const string timeFmt = "{0:yyyy-MM-dd HH:mm:ss}";

        public EnvInfo GetEnvInfo()
        {
            var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();

            var time = DateTime.Now;
            TimeZone localZone = TimeZone.CurrentTimeZone;
            string zone = localZone.StandardName;
            string utcOffset = localZone.GetUtcOffset(time).TotalHours.ToString();

            var timeInfo = new EnvTimeInfo(String.Format(timeFmt, time), zone, utcOffset);

            return name != null ? new EnvInfo(name.ToString(), timeInfo) : new EnvInfo("Unknown", timeInfo);
        }
    }
}
