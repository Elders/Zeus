using CommandLine;
using Newtonsoft.Json;
using System.Collections.Generic;
using Zeus.Linux.Cli.Monitoring;

namespace Zeus.Linux.Cli.Commands
{

    [Verb("check", HelpText = "Performs a check on the pc")]
    public class Check : ICliCommand
    {
        [Option('f', "fail", HelpText = "Fail assertions")]
        public IEnumerable<string> Fails { get; set; }

        public int Run()
        {
            var facility = FacilityResolver.CreateFacility();

            var systemMonitor = new SystemMonitor(facility);

            var status = systemMonitor.GetSystemStatus();

            // System.Console.WriteLine(JsonConvert.SerializeObject(status, Formatting.Indented));
            var queryResult = Assert.Fail(status, Fails);
            System.Console.WriteLine(JsonConvert.SerializeObject(queryResult, Formatting.Indented));
            return 0;
        }
    }
}
