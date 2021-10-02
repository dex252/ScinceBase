namespace Web.Models.Settings
{
    public class SettingsValues
    {
        public decimal CountOfClasses { get; set; }

        public decimal CountOfProperties { get; set; }

        public decimal AwailablePropertyMin { get; set; }

        public decimal AwailablePropertyMax { get; set; }

        public decimal NormalPropertyMin { get; set; }

        public decimal NormalPropertyMax { get; set; }

        public decimal CountOfPeriods { get; set; }

        public decimal AwailablePeriodsMin { get => NormalPropertyMin; }

        public decimal AwailablePeriodsMax { get => NormalPropertyMax; }

        public decimal NormalPeriodsMin { get; set; }

        public decimal NormalPeriodsMax { get; set; }
    }
}
