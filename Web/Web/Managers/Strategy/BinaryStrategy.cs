using System;
using System.Collections.Generic;
using Web.Models.Db.Periods;
using Web.Models.Db.Properties;
using Web.Models.Settings;

namespace Web.Managers.Strategy
{
    public class BinaryStrategy: IStrategy
    {
        private SettingsValues Settings { get; }

        private Random Random { get; }

        public BinaryStrategy(SettingsValues settingsValues)
        {
            Settings = settingsValues;
            Random = new Random();
        }

        public IProperty GetProperty(int index)
        {
            BinaryProperty property = new BinaryProperty();
            property.NormalValue = 50 < Random.Next(100);
            property.CurrentValue = 50 > Random.Next(100);

            property.ValueType = Enums.ValueType.BINARY;
            property.Guid = Guid.NewGuid().ToString();
            property.Name = $"Property-{index}-binary";

            var countPeriods = Settings.CountOfPeriods;
            property.Periods = new List<IPeriod>();
            for (int i = 0; i < countPeriods; i++)
            {
                var previous = i-1 > -1 ? property.Periods[i-1]: null;
                property.Periods.Add(AddPeriod(i, previous as BinaryPeriod));
            }
          

            return property;
        }

        private BinaryPeriod AddPeriod(int index, BinaryPeriod previous)
        {
            var period = new BinaryPeriod();
            period.PeriodNumber = index;
            period.PeriodValue = 50 < Random.Next(100);

            if(previous != null && previous.PeriodValue == period.PeriodValue)
            {
                period.PeriodValue = !period.PeriodValue;
            }

            return period;
        }

    }
}
