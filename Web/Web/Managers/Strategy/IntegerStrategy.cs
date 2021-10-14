using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models.Db2.Periods;
using Web.Models.Db2.Properties;
using Web.Models.Settings;

namespace Web.Managers.Strategy
{
    public class IntegerStrategy : IStrategy
    {
        private SettingsValues Settings { get; }

        private Random Random { get; }

        public IntegerStrategy(SettingsValues settingsValues)
        {
            Settings = settingsValues;
            Random = new Random();
        }

        public IProperty GetProperty(int index)
        {
            IntegerProperty property = new IntegerProperty();

            //Заполнение нормальных значений для свойства
            property.NormalValue = new List<int>();
            var diff = Settings.NormalPropertyMax - Settings.NormalPropertyMin;
            if(diff > 6)
            {
                diff = Random.Next(1, 6);
            }

            for (int i = 0; i < Random.Next(1, diff); i++)
            {
                var value = Random.Next(Settings.NormalPropertyMin, Settings.NormalPropertyMax);
                property.NormalValue.Add(value);
            }
            property.NormalValue = property.NormalValue.Distinct().ToList();

            property.CurrentValue = Random.Next(Settings.AwailablePropertyMin, Settings.AwailablePropertyMax);

            property.ValueType = Enums.ValueType.INTEGER;
            property.Guid = Guid.NewGuid().ToString();
            property.Name = $"Property-{index}-integer";

            var countPeriods = Settings.CountOfPeriods;
            property.Periods = new List<IPeriod>();
            for (int i = 0; i < countPeriods; i++)
            {
                var previous = i - 1 > -1 ? property.Periods[i - 1] : null;
                property.Periods.Add(AddPeriod(i, previous as IntegerPeriod));
            }


            return property;
        }

        private IntegerPeriod AddPeriod(int index, IntegerPeriod previous)
        {
            var period = new IntegerPeriod();
            period.PeriodNumber = index;
            period.PeriodValue = Random.Next(Settings.AwailablePropertyMin, Settings.AwailablePropertyMax);

            if(previous != null)
            {
                var recursiveValidation = 0;
                while(period.PeriodValue == previous.PeriodValue)
                {
                    period.PeriodValue = Random.Next(Settings.AwailablePropertyMin, Settings.AwailablePropertyMax);
                    recursiveValidation++;
                    if(recursiveValidation > 10)
                    {
                        throw new Exception($"Бесконечная рекурсиваня валидация");
                    }
                }
            }

            return period;
        }
    }
}
