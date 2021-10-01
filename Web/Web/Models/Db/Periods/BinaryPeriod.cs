namespace Web.Models.Db.Periods
{
    public class BinaryPeriod: Period
    {
        /// <summary>
        /// Значение в конкретном периоде
        /// </summary>
        public bool PeriodValue { get; set; }
    }
}
