using CommandLine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Zeus.Commands.MachineStatus;
using Zeus.Facility;
using Zeus.JsonAsserts;
using Zeus.Monitors;

namespace Zeus.Commands.ProcessStatus
{
    [Verb("process", HelpText = "Performs a check on a specific process")]
    public class ProcessStatus : ICliCommand
    {
        private JsonSerializerSettings serializerSettings;

        public ProcessStatus()
        {
            serializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
        }

        [Option('f', "format", HelpText = "Formats Output")]
        public bool Format { get; set; }

        [Option('n', "name", HelpText = "Process name")]
        public string Name { get; set; }

        [Option('i', "id", HelpText = "Process ID")]
        public int PId { get; set; }

        [Option('e', "expect", HelpText = "Expectations/Assertions about the system status.")]
        public IEnumerable<string> Expectations { get; set; }

        public int Run()
        {
            ProcessStatusResult programResult = null;
            try
            {
                var facility = FacilityResolver.CreateFacility();

                var systemMonitor = new ProcessMonitor(facility);

                var status = systemMonitor.GetProcessStatus(this.Name);

                programResult = new ProcessStatusResult(status);

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
                programResult = ProcessStatusResult.Failure(ex);
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
