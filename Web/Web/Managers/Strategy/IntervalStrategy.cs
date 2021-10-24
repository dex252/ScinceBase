using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models.Db;
using Web.Models.Db.Properties;
using Web.Models.Settings;
using Attribute = Web.Models.Db.Attribute;

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

        public Attribute GetProperty(int index)
        {
            var property = new Attribute();
            property.Name = $"Property-{index}-interval";
            property.Guid = Guid.NewGuid().ToString();
            property.ValueType = Enums.ValueType.INTERVAL;

            var recursiveValidation = 0;
            SetNoramlValuesAndPeriods(property, recursiveValidation);

            return property;
        }

        private void SetNoramlValuesAndPeriods(Attribute property, int recursiveValidation)
        {
            property.IntervalSettings = new IntervalProperty();
            property.IntervalSettings.NormalInterval = new Interval();

            SetNormalValues(property);

            var countPeriods = Settings.CountOfPeriods;
            property.Periods = new List<Period>(countPeriods);
            for (int i = 0; i < countPeriods; i++)
            {
                var previous = i - 1 > -1 ? property.Periods[i - 1] : null;
                property.Periods.Add(AddPeriod(i, previous, property));
            }

            var isValid = Validate(property);

            if (!isValid)
            {
                if(recursiveValidation > 50)
                {
                    throw new Exception("Бесконечная рекурсивная валидация в интервалах-FillNoramlValuesAndPeriods");
                }

                recursiveValidation++;
                SetNoramlValuesAndPeriods(property, recursiveValidation);
            }
        }

        private bool Validate(Attribute property)
        {
            var normalValues = Enumerable.Range(property.IntervalSettings.NormalInterval.Min, property.IntervalSettings.NormalInterval.Max);
            var isValid = false;

            foreach (var period in property.Periods)
            {
                var min = period.IntervalValue.Min;
                var max = period.IntervalValue.Max;

                if (normalValues.Contains(min) || normalValues.Contains(max))
                {
                    isValid = true;
                }

                if (isValid)
                {
                    return true;
                }
            }

            return false;
        }

        private Period AddPeriod(int index, Period previous, Attribute property)
        {
            var period = new Period();
            period.PeriodNumber = index;
            period.PeriodTime = Random.Next(Settings.NormalPeriodsMin, Settings.NormalPeriodsMax);

            SetPeriod(property, period);

            var recursiveValidation = 0;

            if (previous != null)
            {
                var range = Enumerable.Range(previous.IntervalValue.Min, previous.IntervalValue.Max);

                while(range.Contains(period.IntervalValue.Min) || range.Contains(period.IntervalValue.Max))
                {
                    if (recursiveValidation > 100)
                    {
                        throw new Exception("Бесконечная рекурсивная валидация в интервалах-AddPeriod");
                    }

                    SetPeriod(property, period);
                    recursiveValidation++;
                }
            }

            return period;
        }

        private void SetPeriod(Attribute property, Period period)
        {
            period.IntervalValue = new Interval();

            var interval = period.IntervalValue;
            interval.Min = Random.Next(Settings.AwailablePropertyMin, Settings.AwailablePropertyMax - 1);
            interval.Max = Random.Next(interval.Min + 1, Settings.AwailablePropertyMax);
            var diff = interval.Max - interval.Min;

            var recursiveValidation = 0;

            while (diff > (Settings.AwailablePropertyMax - Settings.AwailablePropertyMin) / 2)
            {

                if (recursiveValidation > 100)
                {
                    throw new Exception("Бесконечная рекурсивная валидация в интервалах-FillPeriod");
                }

                interval.Min = Random.Next(Settings.AwailablePropertyMin, Settings.AwailablePropertyMax - 1);
                interval.Max = Random.Next(interval.Min + 1, Settings.AwailablePropertyMax);
                diff = interval.Max - interval.Min;
                recursiveValidation++;
            }
        }

        private void SetNormalValues(Attribute property)
        {
            var interval = property.IntervalSettings.NormalInterval;
            interval.Min = Random.Next(Settings.NormalPropertyMin, Settings.NormalPropertyMax - 1);
            interval.Max = Random.Next(interval.Min + 1, Settings.NormalPropertyMax);

            if (interval.Max == interval.Min)
            {
                interval.Max++;
            }

            if (interval.Max > Settings.NormalPropertyMax)
            {
                throw new Exception("Максимальное значение на интервале превышено");
            }
        }
    }
}
