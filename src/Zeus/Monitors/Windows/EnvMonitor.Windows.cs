using System.Linq;
using System.Management;

namespace Zeus.Monitors.Windows
{
    public class WindowsEnvMonitor : IEnvMonitor
    {
        public EnvInfo GetEnvInfo()
        {
            var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();

            return name != null ? new EnvInfo(name.ToString()) : new EnvInfo("Unknown");
        }
    }
}
