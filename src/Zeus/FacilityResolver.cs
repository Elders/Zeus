using System.Runtime.InteropServices;
using Zeus.Linux.Cli.Monitoring;
using Zeus.Linux.Cli.UnixMonitors;

namespace Zeus.Linux.Cli
{
    public class FacilityResolver
    {
        public static IMonitorFacility CreateFacility()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return new WindowsFacility();
            else
                return new UnixFacility();
        }
    }
}
