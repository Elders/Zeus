using CommandLine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Zeus.Linux.Cli.Monitoring;

namespace Zeus.Linux.Cli.Commands
{
    public class DSLAssertion
    {
        public string Name { get; set; }

        public string Expected { get; set; }

        public string Actual { get; set; }
    }
}