using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models.Db;
using Web.Models.Db.Properties;
using Web.Models.Settings;
using Attribute = Web.Models.Db.Attribute;

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

        public Attribute GetProperty(int index)
        {
            var property = new Attribute();
            property.Name = $"Property-{index}-integer";
            property.Guid = Guid.NewGuid().ToString();
            property.ValueType = Enums.ValueType.INTEGER;

            FillIntegerNormalValues(property);

            var countPeriods = Random.Next(1, Settings.CountOfPeriods);
            property.Periods = new List<Period>(countPeriods);

            for (int i = 0; i < countPeriods; i++)
            {
                var previous = i - 1 > -1 ? property.Periods[i - 1] : null;
                property.Periods.Add(AddPeriod(i, previous));
            }

            Validate(property);

            return property;
        }

        /// <summary>
        /// Хотя бы одно из значений в периодах должно быть нормальным
        /// </summary>
        /// <param name="property"></param>
        private void Validate(Attribute property)
        {
            var normalValues = property.IntegerSettings.NormalValue;
            var periodValues = property.Periods.Select(e => e.NumberValue);

            var isValid = normalValues.Exists(e => periodValues.Contains(e));

            if (!isValid)
            {
                var index = Random.Next(0, property.Periods.Count - 1);
                var normalIndex = Random.Next(0, normalValues.Count - 1);

                property.Periods[index].NumberValue = normalValues[normalIndex];
            }
        }

        private void FillIntegerNormalValues(Attribute property)
        {
            property.IntegerSettings = new IntegerProperty();
            property.IntegerSettings.NormalValue = new List<int>();

            var diff = Settings.NormalPropertyMax - Settings.NormalPropertyMin;
            if (diff > 6)
            {
                diff = Random.Next(1, 6);
            }

            for (int i = 0; i < Random.Next(1, diff); i++)
            {
                var value = Random.Next(Settings.NormalPropertyMin, Settings.NormalPropertyMax);
                property.IntegerSettings.NormalValue.Add(value);
            }

            property.IntegerSettings.NormalValue = property.IntegerSettings.NormalValue.Distinct().ToList();

        }

        private Period AddPeriod(int index, Period previous)
        {
            var period = new Period();
            period.PeriodNumber = index;
            period.NumberValue = Random.Next(Settings.AwailablePropertyMin, Settings.AwailablePropertyMax);
            period.PeriodTime = Random.Next(Settings.NormalPeriodsMin, Settings.NormalPeriodsMax);

            if (previous != null)
            {
                var recursiveValidation = 0;
                while (period.NumberValue == previous.NumberValue)
                {
                    period.NumberValue = Random.Next(Settings.AwailablePropertyMin, Settings.AwailablePropertyMax);
                    recursiveValidation++;
                    if (recursiveValidation > 20)
                    {
                        throw new Exception($"Бесконечная рекурсиваня валидация");
                    }
                }
            }

            return period;
        }

        //public IProperty GetProperty(int index)
        //{
        //    IntegerProperty property = new IntegerProperty();

        //    //Заполнение нормальных значений для свойства
        //    property.NormalValue = new List<int>();
        //    var diff = Settings.NormalPropertyMax - Settings.NormalPropertyMin;
        //    if(diff > 6)
        //    {
        //        diff = Random.Next(1, 6);
        //    }

        //    for (int i = 0; i < Random.Next(1, diff); i++)
        //    {
        //        var value = Random.Next(Settings.NormalPropertyMin, Settings.NormalPropertyMax);
        //        property.NormalValue.Add(value);
        //    }
        //    property.NormalValue = property.NormalValue.Distinct().ToList();

        //    property.CurrentValue = Random.Next(Settings.AwailablePropertyMin, Settings.AwailablePropertyMax);

        //    property.ValueType = Enums.ValueType.INTEGER;
        //    property.Guid = Guid.NewGuid().ToString();
        //    property.Name = $"Property-{index}-integer";

        //    var countPeriods = Settings.CountOfPeriods;
        //    property.Periods = new List<IPeriod>();
        //    for (int i = 0; i < countPeriods; i++)
        //    {
        //        var previous = i - 1 > -1 ? property.Periods[i - 1] : null;
        //        property.Periods.Add(AddPeriod(i, previous as IntegerPeriod));
        //    }


        //    return property;
        //}

        //private IntegerPeriod AddPeriod(int index, IntegerPeriod previous)
        //{
        //    var period = new IntegerPeriod();
        //    period.PeriodNumber = index;
        //    period.PeriodValue = Random.Next(Settings.AwailablePropertyMin, Settings.AwailablePropertyMax);

        //    if(previous != null)
        //    {
        //        var recursiveValidation = 0;
        //        while(period.PeriodValue == previous.PeriodValue)
        //        {
        //            period.PeriodValue = Random.Next(Settings.AwailablePropertyMin, Settings.AwailablePropertyMax);
        //            recursiveValidation++;
        //            if(recursiveValidation > 10)
        //            {
        //                throw new Exception($"Бесконечная рекурсиваня валидация");
        //            }
        //        }
        //    }

        //    return period;
        //}
    }
}
