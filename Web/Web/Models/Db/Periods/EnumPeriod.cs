using System.Collections.Generic;

namespace Web.Models.Db.Periods
{
    public class EnumPeriod: Period
    {
        /// <summary>
        /// Значения в конкретном периоде
        /// </summary>
        public List<string> PeriodValue { get; set; }
    }
}
