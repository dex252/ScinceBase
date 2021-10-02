namespace Web.Models.Settings
{
    public class SettingsValues
    {
        public int CountOfClasses { get; set; }

        public int CountOfProperties { get; set; }

        public int AwailablePropertyMin { get; set; }

        public int AwailablePropertyMax { get; set; }

        public int NormalPropertyMin { get; set; }

        public int NormalPropertyMax { get; set; }

        public int CountOfPeriods { get; set; }

        public int AwailablePeriodsMin { get => NormalPropertyMin; }

        public int AwailablePeriodsMax { get => NormalPropertyMax; }

        public int NormalPeriodsMin { get; set; }

        public int NormalPeriodsMax { get; set; }
    }
}
