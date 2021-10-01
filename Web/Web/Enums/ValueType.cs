using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Web.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ValueType
    {
        BINARY = 10,
        INTEGER = 20,
        INTERVAL = 30,
        ENUMS = 40
    }
}
