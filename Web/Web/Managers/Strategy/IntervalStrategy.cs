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
            FillNoramlValuesAndPeriods(property, recursiveValidation);

            return property;
        }

        private void FillNoramlValuesAndPeriods(Attribute property, int recursiveValidation)
        {
            property.IntervalSettings = new IntervalProperty();
            property.IntervalSettings.NormalInterval = new Interval();

            FillNormalValues(property);

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
                FillNoramlValuesAndPeriods(property, recursiveValidation);
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

            FillPeriod(property, period);

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

                    FillPeriod(property, period);
                    recursiveValidation++;
                }
            }

            return period;
        }

        private void FillPeriod(Attribute property, Period period)
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

        private void FillNormalValues(Attribute property)
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

        //public IProperty GetProperty(int index)
        //{

        //    IntervalProperty property = new IntervalProperty();

        //    property.NormalIntervals = new List<Interval>();
        //    var interval = new Interval()
        //    {
        //        Min = 1,
        //        Max = 2
        //    };

        //    var diff = Settings.AwailablePropertyMax - Settings.AwailablePropertyMin;

        //    interval.Min = Random.Next(Settings.AwailablePropertyMin, Settings.AwailablePropertyMax - 1);
        //    interval.Max = Random.Next(interval.Min, Settings.AwailablePropertyMax);

        //    if(interval.Max == interval.Min)
        //    {
        //        interval.Max++;
        //    }

        //    if(interval.Max > Settings.AwailablePropertyMax)
        //    {
        //        throw new Exception("Максимальное значение на интервале превышено");
        //    }
        //    property.NormalIntervals.Add(interval);

        //    property.CurrentValue = Random.Next(interval.Min, interval.Max);

        //    property.ValueType = Enums.ValueType.INTERVAL;
        //    property.Guid = Guid.NewGuid().ToString();
        //    property.Name = $"Property-{index}-interval";

        //    var countPeriods = Settings.CountOfPeriods;
        //    property.Periods = new List<IPeriod>();
        //    for (int i = 0; i < countPeriods; i++)
        //    {
        //        var previous = i - 1 > -1 ? property.Periods[i - 1] : null;
        //        property.Periods.Add(AddPeriod(i, previous as IntervalPeriod, property));
        //    }


        //    return property;
        //}

        //private IntervalPeriod AddPeriod(int index, IntervalPeriod previous, IntervalProperty property)
        //{
        //    var period = new IntervalPeriod();
        //    period.PeriodNumber = index;

        //    FillPeriod(property, period);

        //    if (previous != null)
        //    {
        //        while (previous.PeriodValue == period.PeriodValue)
        //        {
        //            FillPeriod(property, period);
        //        }

        //    }

        //    return period;
        //}

        //private void FillPeriod(IntervalProperty property, IntervalPeriod period)
        //{
        //    var recursiveValidation = 0;
        //    var isValid = false;
        //    while (!isValid)
        //    {
        //        period.PeriodValue = Random.Next(Settings.AwailablePropertyMin, Settings.AwailablePropertyMax);
        //        isValid = true;
        //        recursiveValidation++;
        //        foreach (var interval in property.NormalIntervals)
        //        {
        //            if (isValid && (interval.Min > period.PeriodValue || interval.Max < period.PeriodValue))
        //            {
        //                isValid = false;
        //            }
        //        }

        //        if (recursiveValidation > 50)
        //        {
        //            throw new Exception("Бесконечная рекурсивная валидация");
        //        }
        //    }
        //}
    }
}
