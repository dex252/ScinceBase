using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models.Db;
using Web.Models.Db.Properties;
using Web.Models.Settings;
using Attribute = Web.Models.Db.Attribute;

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

        public Attribute GetProperty(int index)
        {
            var property = new Attribute();
            property.Name = $"Property-{index}-binary";
            property.Guid = Guid.NewGuid().ToString();
            property.ValueType = Enums.ValueType.BINARY;
            property.BinarySettings = new BinaryProperty();
            property.BinarySettings.NormalValue = 50 < Random.Next(100);

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
        ///  Хотя бы одно из значений в периодах должно быть нормальным
        /// </summary>
        /// <param name="property"></param>
        private void Validate(Attribute property)
        {
            var normalValue = property.BinarySettings.NormalValue;
            var periodValues = property.Periods.Select(e => e.BinaryValue);

            var isValid = periodValues.Contains(normalValue);

            if (!isValid)
            {
                var index = Random.Next(0, property.Periods.Count - 1);

                property.Periods[index].BinaryValue = normalValue;
            }
        }

        private Period AddPeriod(int index, Period previous)
        {
            var period = new Period();
            period.PeriodNumber = index;
            period.BinaryValue = 50 < Random.Next(100);
            period.PeriodTime = Random.Next(Settings.NormalPeriodsMin, Settings.NormalPeriodsMax);

            if (previous != null && previous.BinaryValue == period.BinaryValue)
            {
                period.BinaryValue = !period.BinaryValue;
            }

            return period;
        }

        //public Attribute GetProperty(int index)
        //{
        //    BinaryProperty property = new BinaryProperty();
        //    property.NormalValue = 50 < Random.Next(100);
        //    property.CurrentValue = 50 > Random.Next(100);

        //    property.ValueType = Enums.ValueType.BINARY;
        //    property.Guid = Guid.NewGuid().ToString();
        //    property.Name = $"Property-{index}-binary";

        //    var countPeriods = Settings.CountOfPeriods;
        //    property.Periods = new List<IPeriod>();
        //    for (int i = 0; i < countPeriods; i++)
        //    {
        //        var previous = i-1 > -1 ? property.Periods[i-1]: null;
        //        property.Periods.Add(AddPeriod(i, previous as BinaryPeriod));
        //    }


        //    return property;
        //}

        //private BinaryPeriod AddPeriod(int index, BinaryPeriod previous)
        //{
        //    var period = new BinaryPeriod();
        //    period.PeriodNumber = index;
        //    period.PeriodValue = 50 < Random.Next(100);

        //    if(previous != null && previous.PeriodValue == period.PeriodValue)
        //    {
        //        period.PeriodValue = !period.PeriodValue;
        //    }

        //    return period;
        //}
    }
}
