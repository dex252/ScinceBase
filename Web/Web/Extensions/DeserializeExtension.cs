using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Web.Models.Db.Periods;
using Web.Models.Db.Properties;

namespace Web.Extensions
{
    public static class DeserializeExtension
    {
        public static List<KeyValuePair<Enums.ValueType, IProperty>> DeserializeProperty(this string value)
        {
            var properties = new List<KeyValuePair<Enums.ValueType, IProperty>>();
            var items = JsonConvert.DeserializeObject<List<KeyValuePair<Enums.ValueType, object>>>(value);

            foreach (var item in items)
            {
                switch (item.Key)
                {
                    case Enums.ValueType.BINARY:
                        {
                            var property = JsonConvert.DeserializeObject<BinaryProperty>(item.Value.ToString());
                            properties.Add(new KeyValuePair<Enums.ValueType, IProperty>(item.Key, property));
                            continue;
                        }
                    case Enums.ValueType.INTEGER:
                        {
                            var property = JsonConvert.DeserializeObject<IntegerProperty>(item.Value.ToString());
                            properties.Add(new KeyValuePair<Enums.ValueType, IProperty>(item.Key, property));
                            continue;
                        }
                    case Enums.ValueType.INTERVAL:
                        {
                            var property = JsonConvert.DeserializeObject<IntervalProperty>(item.Value.ToString());
                            properties.Add(new KeyValuePair<Enums.ValueType, IProperty>(item.Key, property));
                            continue;
                        }
                    case Enums.ValueType.ENUMS:
                        {
                            var property = JsonConvert.DeserializeObject<EnumProperty>(item.Value.ToString());
                            properties.Add(new KeyValuePair<Enums.ValueType, IProperty>(item.Key, property));
                            continue;
                        }
                    default:
                        throw new Exception("Указанный тип свойста не определен");
                }
            }

            return properties;
        }

        public static List<IPeriod> DeserializePeriod(this string value, Enums.ValueType valueType)
        {
            var periods = new List<IPeriod>();

            switch (valueType)
            {
                case Enums.ValueType.BINARY:
                    {
                        var binaries = JsonConvert.DeserializeObject<List<BinaryPeriod>>(value);
                        periods.AddRange(binaries);
                        return periods;
                    }
                case Enums.ValueType.INTEGER:
                    {
                        var binaries = JsonConvert.DeserializeObject<List<IntegerPeriod>>(value);
                        periods.AddRange(binaries);
                        return periods;
                    }
                case Enums.ValueType.INTERVAL:
                    {
                        var binaries = JsonConvert.DeserializeObject<List<IntervalPeriod>>(value);
                        periods.AddRange(binaries);
                        return periods;
                    }
                case Enums.ValueType.ENUMS:
                    {
                        var binaries = JsonConvert.DeserializeObject<List<EnumPeriod>>(value);
                        periods.AddRange(binaries);
                        return periods;
                    }
                default:
                    throw new Exception("Указанный тип свойста не определен");
            }
        }
        
    }
}
