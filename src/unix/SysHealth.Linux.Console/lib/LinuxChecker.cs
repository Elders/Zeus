namespace SysHealth.Linux.Console.lib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Diagnostics;

    using Newtonsoft.Json;


    public class LinuxChecker : SysChecker
    {
        public LinuxChecker(HealthCheckerConfiguration config = null)
        {
            this.config = config;
        }


        private HealthCheckerConfiguration config { get; set; }

        //Physical Memory
        public override int GetTotalPhysicalMemory()
        {
            string totalMemoryAsString;
            int result = 0;

            if (GetInfoFromTop().TryGetValue("memory_total", out totalMemoryAsString))
            {

                if (!int.TryParse(totalMemoryAsString, out result))
                {
                    throw new ArgumentException("Total Physical Memory cannot be parsed to int (" + totalMemoryAsString + ")");
                }
            }

            result = result / this.memory_conversion_value; //Kib to MiB

            return result;
        }

        public override int GetAvailablePhysicalMemory()
        {
            string availableMemoryAsString;
            int result = 0;

            if (GetInfoFromTop().TryGetValue("memory_free", out availableMemoryAsString))
            {
                if (!int.TryParse(availableMemoryAsString, out result))
                {
                    throw new ArgumentException("Total Available Memory cannot be parsed to int (" + availableMemoryAsString + ")");
                }
            }

            result = result / this.memory_conversion_value; //Kib to MiB

            CheckAvailablePhysicalMemoryForCritical(result);

            return result;

        }

        public override int GetUsedPhysicalMemory()
        {
            return this.GetTotalPhysicalMemory() - this.GetAvailablePhysicalMemory();
        }


        //Drives
        public override IEnumerable<DriveMeta> GetAllDrives()
        {
            throw new NotImplementedException();
        }

        public override DriveMeta GetDriveInfo(string driveName)
        {
            var result = new DriveMeta();
            result.Name = "total";

            var driveInfo = GetDriveInfoFromDf();

            var driveTotalAsString = "";
            if (driveInfo.TryGetValue("drive_total", out driveTotalAsString))
            {

                decimal driveTotalAsDecimal = 0;
                if (decimal.TryParse(driveTotalAsString, out driveTotalAsDecimal))
                {
                    result.TotalSize = driveTotalAsDecimal * this.memory_conversion_value;
                }

            }

            var driveFreeAsString = "";
            if (driveInfo.TryGetValue("drive_free", out driveFreeAsString))
            {
                decimal driveFreeAsDecimal = 0;
                if (decimal.TryParse(driveFreeAsString, out driveFreeAsDecimal))
                {
                    result.TotalFreeSpace = driveFreeAsDecimal * this.memory_conversion_value; // GiB to MiB
                }

            }

            CheckDrivesForCritical(new List<DriveMeta>() { result });

            return result;
        }


        //CPU
        public override float GetCPUUsage(string processName = "_Total")
        {
            string availableCpuAsString;
            float result = 0;

            if (GetInfoFromTop().TryGetValue("cpu_free", out availableCpuAsString))
            {
                if (!float.TryParse(availableCpuAsString, out result))
                {
                    throw new ArgumentException("CPU usage cannot be parsed to float (" + availableCpuAsString + ")");
                }
            }

            result = 100 - (result / 10);

            CheckCPUForCritical(result);
            return result;
        }

        public override string GetFullStatus()
        {
            var result = new Status();

            //PHYSICAL MEMORY
            result.physical_memory_total = this.GetTotalPhysicalMemory();
            result.physical_memory_used = this.GetUsedPhysicalMemory();
            result.physical_memory_free = this.GetTotalPhysicalMemory() - this.GetUsedPhysicalMemory();

            result.cpu_usage = this.GetCPUUsage();

            result.drives_info.Add(GetDriveInfo("total"));

            return JsonConvert.SerializeObject(result);
        }

        private Dictionary<string, string> GetInfoFromTop()
        {
            var result = new Dictionary<string, string>();
            var top = GetCommandResultToString("top", "-b -n 1");
            var topRows = top.Split('\n').Where(x => !string.IsNullOrEmpty(x)).ToArray();

            for (int row = 1; row < topRows.Length; row++)
            {
                var currentRow = topRows[row].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();

                for (int col = 0; col < currentRow.Length; col++)
                {
                    //CPU
                    if (row == 2)
                    {
                        if (currentRow[col].Contains("id"))
                        {
                            var freeCpu = currentRow[col - 1];

                            result.Add("cpu_free", freeCpu);
                            break;
                        }
                    }

                    //Memory
                    if (row == 3)
                    {
                        if (currentRow[col].Contains("KiB") && currentRow[col + 1].Contains("Mem"))
                        {
                            var totalMemory = currentRow[col + 2];
                            var usedMemory = currentRow[col + 4];
                            var freeMemory = currentRow[col + 6];

                            result.Add("memory_total", totalMemory);
                            result.Add("memory_used", usedMemory);
                            result.Add("memory_free", freeMemory);

                            break;
                        }
                    }
                }
            }

            return result;
        }

        private Dictionary<string, string> GetDriveInfoFromDf()
        {
            var result = new Dictionary<string, string>();

            var dfResult = GetCommandResultToString("df", "-h --total").Split('\n').FirstOrDefault(str => str.Contains("total")).Split(new char[] { ' ' }).Where(x => !string.IsNullOrEmpty(x)).ToArray();

            var driveTotal = dfResult[1].Replace("G", "");
            var driveUsed = dfResult[2].Replace("G", "");
            var driveFree = dfResult[3].Replace("G", "");

            result.Add("drive_total", driveTotal);
            result.Add("drive_used", driveUsed);
            result.Add("drive_free", driveFree);


            return result;
        }

        private string GetCommandResultToString(string command, string commandArgs = "")
        {
            try
            {
                Process testProcess = new Process();
                string result = "";

                testProcess.StartInfo.FileName = command;

                testProcess.StartInfo.UseShellExecute = false;
                testProcess.StartInfo.RedirectStandardInput = true;
                testProcess.StartInfo.RedirectStandardOutput = true;

                if (!string.IsNullOrEmpty(commandArgs))
                {
                    testProcess.StartInfo.Arguments = commandArgs;
                }


                testProcess.Start();

                StreamReader sOut = testProcess.StandardOutput;

                result = sOut.ReadToEnd();

                if (!testProcess.HasExited)
                {
                    testProcess.Kill();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("System cannot find \"" + command + "\" command to execute! Pleasee check your runtime environment");
            }

        }


    }
}