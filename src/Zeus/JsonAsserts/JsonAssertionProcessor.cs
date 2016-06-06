using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Zeus.JsonAsserts
{
    public class JsonAssertionProcessor
    {
        JToken queryObject;

        public JsonAssertionProcessor(object instance)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            queryObject = JToken.Parse(JsonConvert.SerializeObject(instance, Formatting.None, settings));
        }

        public ExpectiationResult Assert(Expectation expectation)
        {
            var query = expectation.QueryParts;
            var instance = queryObject;
            try
            {
                while (query.Count > 0)
                {
                    var item = query.Dequeue();
                    var next = instance[item.ToLowerInvariant()];
                    if (next != null)
                        instance = next;
                    else
                    {
                        var path = string.IsNullOrEmpty(instance.Path) ? "object" : instance.Path;
                        return ExpectiationResult.Error(expectation, path + " does not contain " + item);
                    }

                }
            }
            catch (Exception ex)
            {
                return ExpectiationResult.Error(expectation, "Failed to parse path:" + expectation.Query + " in " + instance.Path, ex.GetType().Name, ex.Message, ex.StackTrace);
            }
            int result = 0;
            try
            {
                result = JTokenComparer.Compare(instance, expectation.ExpectedValue);
            }
            catch (Exception ex)
            {
                return ExpectiationResult.Error(expectation, "Failed compare values:", ex.GetType().Name, ex.Message, ex.StackTrace);
            }

            if (expectation.Assertion != "above" && expectation.Assertion != "below")
                return ExpectiationResult.Error(expectation, "Invalid assertion " + expectation.Assertion);
            if (result == 1 && expectation.Assertion == "above")
                return ExpectiationResult.Success(expectation);
            else if (result == -1 && expectation.Assertion == "below")
                return ExpectiationResult.Success(expectation);
            else
                return ExpectiationResult.Error(expectation, $"Expectation '{expectation.ToString()}' failed");
        }


    }
}