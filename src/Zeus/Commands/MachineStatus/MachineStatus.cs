using CommandLine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Zeus.Linux.Cli.JsonAsserts;
using Zeus.Linux.Cli.Monitoring;

namespace Zeus.Linux.Cli.Commands
{

    [Verb("machine", HelpText = "Performs a check on the pc")]
    public class MachineStatus : ICliCommand
    {
        private JsonSerializerSettings serializerSettings;

        public MachineStatus()
        {
            serializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
        }


        [Option('e', "expect", HelpText = "Expectations/Assertions about the system status.")]
        public IEnumerable<string> Expectations { get; set; }

        [Option('f', "format", HelpText = "Formats Output")]
        public bool Format { get; set; }

        public int Run()
        {
            MachineStatusResult programResult = null;
            try
            {
                var facility = FacilityResolver.CreateFacility();

                var systemMonitor = new SystemMonitor(facility);

                var status = systemMonitor.GetSystemStatus();

                programResult = new MachineStatusResult(status);

                var processor = new JsonAssertionProcessor(programResult);
                foreach (var expectation in Expectations.Select(x => Expectation.Parse(x)))
                {
                    var expectationResult = processor.Assert(expectation);
                    if (expectationResult.Failed)
                        programResult.AddErrors(expectationResult.Errors);
                }
            }
            catch (Exception ex)
            {
                programResult = MachineStatusResult.Failure(ex);
            }
            finally
            {
                var formating = Format ? Formatting.Indented : Formatting.None;
                System.Console.WriteLine(JsonConvert.SerializeObject(programResult, formating, serializerSettings));
            }
            if (programResult == null || programResult.Error)
                return 1;
            else
                return 0;
        }
    }
}
