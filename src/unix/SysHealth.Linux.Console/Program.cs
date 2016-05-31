

namespace SysHealth.Linux.Console
{
    using System;

    using Newtonsoft.Json;

    using lib;

    public class Program
    {

        static int Main(string[] args)
        {
            var heathConfig = GetHealthConfig(args);
            var healthChecker = new LinuxChecker(heathConfig);

            try
            {
                Console.WriteLine(healthChecker.GetFullStatus());
            }
            catch (Exception ex)
            {
                var exception = JsonConvert.SerializeObject(new { check_date = DateTime.UtcNow, message = ex.Message });
                Console.WriteLine(exception);
                return 1;
            }
            Console.ReadLine();
            return 0;
        }

        private static HealthCheckerConfiguration GetHealthConfig(string[] args)
        {
            var result = new HealthCheckerConfiguration();

            if (args.Length == 0)
            {
                result.CriticalPointCPU = 95;
                result.CriticalPointDriveFreeSpace = 1000;
                result.CriticalPointAvailablePhysicalMemory = 100;
            }
            else
            {
                foreach (var item in args)
                {
                    if (item.Contains("crit-cpu="))
                    {
                        float critCpu;
                        var passedParamValue = item.Substring(item.IndexOf('=') + 1);
                        if (float.TryParse(passedParamValue, out critCpu))
                        {
                            result.CriticalPointCPU = critCpu;
                        }
                    }
                    else if (item.Contains("crit-mem="))
                    {
                        ulong critMemory;
                        var passedParamValue = item.Substring(item.IndexOf('=') + 1);
                        if (ulong.TryParse(passedParamValue, out critMemory))
                        {
                            result.CriticalPointAvailablePhysicalMemory = critMemory;
                        }
                    }
                    else if (item.Contains("crit-space="))
                    {
                        ulong critDriveSpace;
                        var passedParamValue = item.Substring(item.IndexOf('=') + 1);
                        if (ulong.TryParse(passedParamValue, out critDriveSpace))
                        {
                            result.CriticalPointAvailablePhysicalMemory = critDriveSpace;
                        }
                    }
                }
            }


            return result;

        }
    }
}
