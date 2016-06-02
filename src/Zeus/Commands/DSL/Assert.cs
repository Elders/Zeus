using CommandLine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Zeus.Linux.Cli.Monitoring;

namespace Zeus.Linux.Cli.Commands
{

    public class Assert
    {
        public static DSLAssertionQueryResult Fail(SystemStatus status, IEnumerable<string> asserions)
        {
            var dslObject = new DSLQueryObject(status);
            var result = new DSLAssertionQueryResult(dslObject);
            JToken queryObject = JObject.Parse(JsonConvert.SerializeObject(dslObject));
            List<DSLAssertion> assertionResult = new List<DSLAssertion>();
            foreach (var assertion in asserions)
            {
                try
                {
                    bool success = true;
                    var splited = assertion.Split(':');
                    var query = splited[0].Split('.').ToList();
                    var equality = splited[1];
                    var expectedValue = decimal.Parse(splited[2]);
                    var valueForAssertion = queryObject;
                    foreach (var item in query)
                    {
                        valueForAssertion = valueForAssertion[item];
                    }
                    var actualValue = valueForAssertion.Value<decimal>();

                    if (equality == "below")
                        success = actualValue < expectedValue;
                    else if (equality == "above")
                        success = actualValue > expectedValue;
                    else
                        throw new InvalidOperationException(String.Format("Invalid comparer '{0}'", equality));

                    if (success)
                        result.Errors.Add(new DSLAssertion() { Name = assertion, Actual = actualValue.ToString(), Expected = expectedValue.ToString() });
                    else
                        result.Passes.Add(new DSLAssertion() { Name = assertion, Actual = actualValue.ToString(), Expected = expectedValue.ToString() });



                }
                catch (Exception ex)
                {
                    result.Errors.Add(new DSLAssertion() { Name = ex.GetType().Name, Actual = assertion, Expected = ex.Message });
                }
            }

            return result;
        }
        public class DSLAssertionQueryResult
        {
            public DSLAssertionQueryResult(DSLQueryObject query)
            {
                Query = query;
                Passes = new List<DSLAssertion>();
                Errors = new List<DSLAssertion>();
            }

            public List<DSLAssertion> Errors { get; private set; }

            public List<DSLAssertion> Passes { get; private set; }

            public DSLQueryObject Query { get; private set; }
        }

        public class DSLQueryObject
        {
            public DSLQueryObject(SystemStatus status)
            {
                cpu = new DSLCpu(status.CpuInfo);
                hdd = status.DrivesInfo.ToDictionary(x => x.VolumeName, x => new DSLDrive(x));
                ram = new DSLRam(status.MemoryInfo);
            }

            public DSLCpu cpu { get; set; }

            public Dictionary<string, DSLDrive> hdd { get; set; }

            public DSLRam ram { get; set; }
        }
    }
}