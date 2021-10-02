using System;
using Web.Models.Db.Periods;
using Web.Models.Db.Properties;
using Web.Models.Settings;

namespace Web.Managers.Strategy
{
    public class IntegerStrategy : IStrategy
    {
        private SettingsValues SettingsValues { get; }

        private Random Random { get; }

        public IntegerStrategy(SettingsValues settingsValues)
        {
            SettingsValues = settingsValues;
            Random = new Random();
        }

        public IProperty GetProperty(int index)
        {
            IntegerProperty property = new IntegerProperty();
            //TODO: остановился здесь
            property.NormalValue = 50 < Random.Next(100);

            property.ValueType = Enums.ValueType.BINARY;
            property.Guid = Guid.NewGuid().ToString();
            property.Name = $"Property-{index}";

            var countPeriods = SettingsValues.CountOfPeriods;
            property.Periods = new List<IPeriod>();
            for (int i = 0; i < countPeriods; i++)
            {
                property.Periods.Add(AddPeriod(i));
            }


            return property;
        }

        private IntegerPeriod AddPeriod(int index)
        {
            var period = new IntegerPeriod();
            period.PeriodNumber = index;
           

            return period;
        }
    }
}
