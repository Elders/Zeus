namespace SysHealth.Linux.Console.lib
{
    using System;
    using System.Collections.Generic;

    public class Status
    {
        public Status()
        {
            this.drives_info = new List<DriveMeta>();
            this.check_date = DateTime.UtcNow;
        }

        public DateTime check_date { get; set; }

        public int physical_memory_total { get; set; }
        public int physical_memory_free { get; set; }
        public int physical_memory_used { get; set; }

        public double cpu_usage { get; set; }

        public ICollection<DriveMeta> drives_info { get; set; }

    }
}
