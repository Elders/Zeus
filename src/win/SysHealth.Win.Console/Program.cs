

namespace SysHealth.Win.Console
{
    using System;

    using SysHealth.Win.Lib;

    class Program
    {
        static int Main(string[] args)
        {
            var heathConfig = GetHealthConfig(args);
            var healthChecker = new WinChecker(heathConfig);

            try
            {
                Console.WriteLine(healthChecker.GetFullStatus());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            return 0;

        }

        private static HealthCheckerConfiguration GetHealthConfig(string[] args)
        {
            var result = new HealthCheckerConfiguration();

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

            return result;
        }
    }
}
