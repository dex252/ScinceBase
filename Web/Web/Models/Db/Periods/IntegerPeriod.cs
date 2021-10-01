namespace Web.Models.Db.Periods
{
    public class IntegerPeriod: Period
    {
        /// <summary>
        /// Значение в конкретном периоде
        /// </summary>
        public int CurrentValue { get; set; }
    }
}
