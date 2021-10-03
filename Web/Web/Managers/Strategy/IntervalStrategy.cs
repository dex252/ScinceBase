using System;
using System.Collections.Generic;
using Web.Models.Db.Periods;
using Web.Models.Db.Properties;
using Web.Models.Settings;

namespace Web.Managers.Strategy
{
    public class IntervalStrategy: IStrategy
    {
        private SettingsValues Settings { get; }

        private Random Random { get; }

        public IntervalStrategy(SettingsValues settingsValues)
        {
            Settings = settingsValues;
            Random = new Random();
        }

        public IProperty GetProperty(int index)
        {
           
            IntervalProperty property = new IntervalProperty();

            property.NormalIntervals = new List<Interval>();
            var interval = new Interval()
            {
                Min = 1,
                Max = 2
            };

            var diff = Settings.AwailablePropertyMax - Settings.AwailablePropertyMin;
            
            interval.Min = Random.Next(Settings.AwailablePropertyMin, Settings.AwailablePropertyMax - 1);
            interval.Max = Random.Next(interval.Min, Settings.AwailablePropertyMax);

            if(interval.Max == interval.Min)
            {
                interval.Max++;
            }

            if(interval.Max > Settings.AwailablePropertyMax)
            {
                throw new Exception("Максимальное значение на интервале превышено");
            }
            property.NormalIntervals.Add(interval);

            property.CurrentValue = Random.Next(interval.Min, interval.Max);

            property.ValueType = Enums.ValueType.INTERVAL;
            property.Guid = Guid.NewGuid().ToString();
            property.Name = $"Property-{index}-interval";

            var countPeriods = Settings.CountOfPeriods;
            property.Periods = new List<IPeriod>();
            for (int i = 0; i < countPeriods; i++)
            {
                var previous = i - 1 > -1 ? property.Periods[i - 1] : null;
                property.Periods.Add(AddPeriod(i, previous as IntervalPeriod, property));
            }


            return property;
        }

        private IntervalPeriod AddPeriod(int index, IntervalPeriod previous, IntervalProperty property)
        {
            var period = new IntervalPeriod();
            period.PeriodNumber = index;

            FillPeriod(property, period);

            if (previous != null)
            {
                while (previous.PeriodValue == period.PeriodValue)
                {
                    FillPeriod(property, period);
                }
               
            }

            return period;
        }

        private void FillPeriod(IntervalProperty property, IntervalPeriod period)
        {
            var recursiveValidation = 0;
            var isValid = false;
            while (!isValid)
            {
                period.PeriodValue = Random.Next(Settings.AwailablePropertyMin, Settings.AwailablePropertyMax);
                isValid = true;
                recursiveValidation++;
                foreach (var interval in property.NormalIntervals)
                {
                    if (isValid && (interval.Min > period.PeriodValue || interval.Max < period.PeriodValue))
                    {
                        isValid = false;
                    }
                }

                if (recursiveValidation > 50)
                {
                    throw new Exception("Бесконечная рекурсивная валидация");
                }
            }
        }
    }
}
