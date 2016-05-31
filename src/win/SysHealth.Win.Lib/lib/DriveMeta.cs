using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysHealth.Win.Lib
{
    public class DriveMeta
    {
        public DriveMeta()
        {

        }

        public DriveMeta(string name, long totalSize, long totalFreeSpace)
        {
            this.Name = name;
            this.TotalSize = totalSize;
            this.TotalFreeSpace = totalFreeSpace;
        }


        public bool IsReady { get; set; }

        public string Name { get; set; }

        public long TotalFreeSpace { get; set; }

        public long TotalSize { get; set; }
    }
}
