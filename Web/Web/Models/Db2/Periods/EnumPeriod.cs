using System.Collections.Generic;

namespace Web.Models.Db2.Periods
{
    public class EnumPeriod: PeriodV2
    {
        /// <summary>
        /// Значения в конкретном периоде
        /// </summary>
        public List<string> PeriodValue { get; set; }
    }
}
