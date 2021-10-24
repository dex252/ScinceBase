using Newtonsoft.Json;
using System.Collections.Generic;
using Web.Models.Db.Properties;

namespace Web.Models.Db
{
    public class Period
    {
        public int PeriodNumber { get; set; }

        public int PeriodTime { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int NumberValue { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [System.Obsolete]
        public List<Interval> IntervalValues { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Interval IntervalValue { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool BinaryValue { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> EnumValues { get; set; }
    }
}
