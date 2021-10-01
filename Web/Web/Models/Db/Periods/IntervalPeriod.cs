﻿namespace Web.Models.Db.Periods
{
    public class IntervalPeriod: Period
    {
        /// <summary>
        /// Значение в конкретном периоде
        /// </summary>
        public int CurrentValue { get; set; }
    }
}