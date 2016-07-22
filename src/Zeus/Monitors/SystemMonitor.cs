using System.Collections.Generic;
using Zeus.Facility;

namespace Zeus.Monitors
{
    public class SystemMonitor
    {
        private IMonitorFacility facility;

        public SystemMonitor(IMonitorFacility facility)
        {
            this.facility = facility;
        }

        public SystemStatus GetSystemStatus()
        {
            var envMonitor = facility.Resolve<IEnvMonitor>();
            var cpuMonitor = facility.Resolve<ICpuMonitor>();
            var memoryInfo = facility.Resolve<IMemoryMonitor>();
            var hddMonitor = facility.Resolve<IHDDMonitor>();

            return new SystemStatus(cpuMonitor.GetCpuUsage(), hddMonitor.GetHDDUsage(), memoryInfo.GetMemoryUsage(), envMonitor.GetEnvInfo());
        }
    }

    public class SystemStatus
    {
        public SystemStatus(CpuInfo cpuInfo, IEnumerable<DriveInfo> drivesInfo, MemoryInfo memoryInfo, EnvInfo envInfo)
        {
            CpuInfo = cpuInfo;
            DrivesInfo = drivesInfo;
            MemoryInfo = memoryInfo;
            EnvInfo = envInfo;
        }

        public CpuInfo CpuInfo { get; set; }

        public IEnumerable<DriveInfo> DrivesInfo { get; set; }

        public MemoryInfo MemoryInfo { get; set; }

        public EnvInfo EnvInfo { get; set; }
    }
}
