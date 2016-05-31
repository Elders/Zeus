using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysHealth.Win.Lib
{
    public class HealthCheckerConfiguration
    {
        public HealthCheckerConfiguration()
        {

        }

        public ulong? CriticalPointAvailablePhysicalMemory { get; set; }
        public long? CriticalPointDriveFreeSpace { get; set; }
        public float? CriticalPointCPU { get; set; }
    }
}
