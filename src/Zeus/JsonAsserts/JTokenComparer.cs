using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Zeus.JsonAsserts
{
    public static class JTokenComparer
    {
        public static int Compare(JToken token, object expectedValue)
        {
            switch (token.Type)
            {
                case JTokenType.Integer:
                    return Compare<int>(token, expectedValue);
                case JTokenType.Float:
                    return Compare<decimal>(token, expectedValue);
                case JTokenType.Boolean:
                    return Compare<bool>(token, expectedValue);
                default:
                    throw new NotSupportedException("Not supported type: " + token.Type);
            }

        }
        public static int Compare<T>(JToken token, object value)
        {

            return Comparer<T>.Default.Compare(token.Value<T>(), (T)Convert.ChangeType(value, typeof(T)));
        }
    }
}