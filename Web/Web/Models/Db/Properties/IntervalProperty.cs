using Newtonsoft.Json;
using System.Collections.Generic;

namespace Web.Models.Db.Properties
{
    public class IntervalProperty
    {
        /// <summary>
        /// Текущее значение
        /// </summary>
        public int CurrentValue { get; set; }

        /// <summary>
        /// Нормальные значения интервала
        /// </summary>
        public Interval NormalInterval { get; set; }

        /// <summary>
        /// Нормальные значения интервалов
        /// </summary>
        [System.Obsolete]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Interval> NormalIntervals { get; set; }

    }

    public class Interval
    {
        public int Min { get; set; }

        public int Max { get; set; }
    }
}
