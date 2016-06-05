using Zeus.Monitors;

namespace Zeus.Commands.MachineStatus
{
    public class DriveResult
    {
        public DriveResult(DriveInfo info)
        {
            Free = info.FreeSpace;
            Occupied = info.TotalSpace - Free;
            Total = info.TotalSpace;
            Usage = info.UsedSpaceInPercent;
        }
        public decimal Free { get; set; }
        public decimal Total { get; set; }
        public decimal Occupied { get; set; }
        public decimal Usage { get; set; }
    }
}