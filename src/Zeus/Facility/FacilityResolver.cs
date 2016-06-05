namespace Zeus.Facility
{
    public partial class FacilityResolver
    {
        public static IMonitorFacility CreateFacility()
        {
            return new Facility();
        }
    }
}
