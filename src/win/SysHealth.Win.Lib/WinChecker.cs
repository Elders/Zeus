namespace SysHealth.Win.Lib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using System.Diagnostics;

    using Newtonsoft.Json;

    using Microsoft.VisualBasic.Devices;

    using SysHealth;
    using lib;
    public class WinChecker : SystemChecker
    {
        private ComputerInfo pcInfo;

        public WinChecker(HealthCheckerConfiguration config = null)
        {
            this.pcInfo = new ComputerInfo();
            this.config = config;
        }


        private HealthCheckerConfiguration config { get; set; }

        //Physical Memory
        public override ulong GetTotalPhysicalMemory()
        {
            var result = this.pcInfo.TotalPhysicalMemory / (ulong)this.bytesPerMebibyte;

            return result;
            //return 1;
        }

        public override ulong GetAvailablePhysicalMemory()
        {
            //PerformanceCounter cpuCounter = new PerformanceCounter("Memory", "Available MBytes", String.Empty);

            //// will always start at 0
            //float firstValue = cpuCounter.NextValue();
            //System.Threading.Thread.Sleep(1000);
            //// now matches task manager reading
            //float secondValue = cpuCounter.NextValue();

            var result = this.pcInfo.AvailablePhysicalMemory / (ulong)this.bytesPerMebibyte;

            CheckAvailablePhysicalMemoryForCritical(result);

            //return secondValue;
            return result;

        }

        public override ulong GetUsedPhysicalMemory()
        {
            //throw new NotImplementedException();
            return this.GetTotalPhysicalMemory() - this.GetAvailablePhysicalMemory();
        }

        //Drives
        public override IEnumerable<DriveMeta> GetAllDrives()
        {
            var allDrives = DriveInfo.GetDrives().Where(d => d.IsReady).ToArray();
            var result = new List<DriveMeta>();

            foreach (var drive in allDrives)
            {
                result.Add(new DriveMeta(drive.Name, drive.TotalSize, drive.TotalFreeSpace));
            }

            //CheckDrivesForCritical(result);

            return result;
        }

        public override DriveMeta GetDriveInfo(string driveName)
        {
            var result = this.GetAllDrives().Where(drive => drive.Name == driveName);

            if (result.Count() > 0)
                CheckDrivesForCritical(result.ToArray());

            return result != null ? result.Single() : null;
        }

        //CPU
        public override float GetCPUUsage(string processName = "_Total")
        {
            System.Diagnostics.PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", processName);

            // will always start at 0
            float firstValue = cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            // now matches task manager reading
            float secondValue = cpuCounter.NextValue();

            CheckCPUForCritical(secondValue);
            return secondValue;
        }

        public override string GetFullStatus()
        {
            var result = new Status();

            //PHYSICAL MEMORY
            result.physical_memory_total = this.GetTotalPhysicalMemory();
            result.physical_memory_used = this.GetUsedPhysicalMemory();
            result.physical_memory_free = this.GetTotalPhysicalMemory() - this.GetUsedPhysicalMemory();

            result.cpu_usage = this.GetCPUUsage();

            foreach (var drive in this.GetAllDrives())
            {
                result.drives_info.Add(GetDriveInfo(drive.Name));
            }



            return JsonConvert.SerializeObject(result);
        }
    }
}
