using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysHealth.Win.Lib.lib
{
    class Status
    {
        public Status()
        {
            this.drives_info = new List<DriveMeta>();
            this.check_date = DateTime.UtcNow;
        }

        public DateTime check_date { get; set; }

        public ulong physical_memory_total { get; set; }
        public ulong physical_memory_free { get; set; }
        public ulong physical_memory_used { get; set; }

        public double cpu_usage { get; set; }

        public ICollection<DriveMeta> drives_info { get; set; }
    }
}
