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
        /// Нормальные значения интервалов
        /// </summary>
        public List<Interval> Intervals { get; set; }
        
    }

    public class Interval
    {
        public int Min { get; set; }

        public int Max { get; set; }
    }
}
