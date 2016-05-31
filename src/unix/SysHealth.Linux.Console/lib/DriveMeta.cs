namespace SysHealth.Linux.Console.lib
{
    public class DriveMeta
    {
        public DriveMeta()
        {

        }

        public DriveMeta(string name, decimal totalSize, decimal totalFreeSpace)
        {
            this.Name = name;
            this.TotalSize = totalSize;
            this.TotalFreeSpace = totalFreeSpace;
        }

        public string Name { get; set; }

        public decimal TotalFreeSpace { get; set; }

        public decimal TotalSize { get; set; }
    }
}
