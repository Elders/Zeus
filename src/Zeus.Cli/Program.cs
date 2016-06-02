using Zeus.Linux.Cli.UnixMonitors;
using Zeus.Linux.Cli.Monitoring;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using Zeus.Linux.Cli.Commands;
using CommandLine;

namespace Zeus.Linux.Cli
{
    public class Program
    {
        public static int MainOld(string[] args)
        {
            var heathConfig = GetHealthConfig(args);

            var unixFacility = new UnixFacility(args);
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            var systemMonitor = new SystemMonitor(unixFacility);

            var status = systemMonitor.GetSystemStatus();

            System.Console.WriteLine(JsonConvert.SerializeObject(status, Formatting.Indented));

            return 0;
        }
        public static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<Check>(args)
                                  .MapResult((Check cmd) => cmd.Run(), errs => 1);
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
