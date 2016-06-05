using Zeus.Linux.Cli.Monitoring;

namespace Zeus.Linux.Cli
{
    public partial class FacilityResolver
    {
        public static IMonitorFacility CreateFacility()
        {
            return new Facility();
        }
    }
}
