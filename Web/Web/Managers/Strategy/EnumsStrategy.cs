using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models.Db;
using Web.Models.Db.Properties;
using Web.Models.Settings;
using Attribute = Web.Models.Db.Attribute;

namespace Web.Managers.Strategy
{
    public class EnumsStrategy : IStrategy
    {
        private SettingsValues Settings { get; }

        private EnumsValue EnumsValues { get; }

        private Random Random { get; }
        public EnumsStrategy(SettingsValues settingsValues, List<EnumsValue> enumsValues)
        {
            Settings = settingsValues;
            Random = new Random();
            EnumsValues = enumsValues[Random.Next(0, enumsValues.Count - 1)];
        }

        public Attribute GetProperty(int index)
        {
            var property = new Attribute();
            property.Name = $"Property-{index}-enums";
            property.Guid = Guid.NewGuid().ToString();
            property.ValueType = Enums.ValueType.ENUMS;

            var normalValues = EnumsValues.Values.OrderBy(e => Random.Next()).Take(Random.Next(1, EnumsValues.Values.Count - 1));

            SetNormalValues(property, normalValues);
            SetPeriods(property, normalValues);

            Validate(property);

            return property;
        }

        private void Validate(Attribute property)
        {
            var allValues = new List<string>();
            foreach (var period in property.Periods)
            {
                var values = period.EnumValues;
                allValues.AddRange(values);
            }

            allValues = allValues.Distinct().ToList();
            var otherValues = property.EnumSettings.NormalValues.Where(e => !allValues.Contains(e));

            if(otherValues.Count() == 0)
            {
                return;
            }

            foreach (var value in otherValues)
            {
                var index = Random.Next(0, property.Periods.Count - 1);
                property.Periods[index].EnumValues.Add(value);
            }
        }

        private void SetPeriods(Attribute property, IEnumerable<string> normalValues)
        {
            var countPeriods = Random.Next(1, Settings.CountOfPeriods);
            property.Periods = new List<Period>(countPeriods);

            for (int i = 0; i < countPeriods; i++)
            {
                var previous = i - 1 > -1 ? property.Periods[i - 1] : null;
                property.Periods.Add(AddPeriod(i, previous));
            }
        }

        private Period AddPeriod(int index, Period previous)
        {
            var period = new Period();
            period.PeriodNumber = index;
            period.EnumValues = new List<string>();
            period.PeriodTime = Random.Next(Settings.NormalPeriodsMin, Settings.NormalPeriodsMax);

            if (previous == null)
            {
                var periodValues = EnumsValues.Values.OrderBy(e => Random.Next()).Take(Random.Next(1, EnumsValues.Values.Count - 1));
                period.EnumValues = periodValues.ToList();
                return period;
            }

            var previousValues = previous.EnumValues;
            var otherValues = EnumsValues.Values.Where(e => !previousValues.Contains(e)).ToList();

            if (otherValues.Count == 0)
            {
                throw new Exception($"Критическая ошибка в периодах перечислений, число оставшихся значений перечисления {EnumsValues.Name} равно 0");
            }

            var values = otherValues.OrderBy(e => Random.Next()).Take(Random.Next(1, otherValues.Count));
            period.EnumValues = values.ToList();

            return period;
        }

        private void SetNormalValues(Attribute property, IEnumerable<string> normalValues)
        {
            property.EnumSettings = new EnumProperty();
            property.EnumSettings.NormalValues = new List<string>();

            
            property.EnumSettings.NormalValues = normalValues.ToList();
            property.EnumSettings.Name = EnumsValues.Name;
            property.EnumSettings.Guid = EnumsValues.Guid;
        }
    }
}
