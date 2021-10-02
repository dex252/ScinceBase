using System;
using System.Collections.Generic;
using Web.Models.Db.Periods;
using Web.Models.Db.Properties;
using Web.Models.Settings;

namespace Web.Managers.Strategy
{
    public class BinaryStrategy: IStrategy
    {
        private SettingsValues SettingsValues { get; }

        private Random Random { get; }

        public BinaryStrategy(SettingsValues settingsValues)
        {
            SettingsValues = settingsValues;
            Random = new Random();
        }

        public IProperty GetProperty(int index)
        {
            BinaryProperty property = new BinaryProperty();
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

        private BinaryPeriod AddPeriod(int index)
        {
            var period = new BinaryPeriod();
            period.PeriodNumber = index;
            period.PeriodValue = 50 < Random.Next(100);

            return period;
        }

    }
}
