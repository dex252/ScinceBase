using Dapper;
using Newtonsoft.Json;
using System.Collections.Generic;
using Computed = Dapper.Contrib.Extensions.ComputedAttribute;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;
using TableAttribute = Dapper.Contrib.Extensions.TableAttribute;
using Web.Enums;
using Web.Models.Db.Properties;

namespace Web.Models.Db
{
    [Table("properties")]
    public class Attribute
    {
        [Key]
        public decimal? Id { get; set; }

        [Column("guid")]
        public string Guid { get; set; }

        [Column("name")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [Column("type")]
        [JsonProperty(Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public ValueType ValueType { get; set; }

        [Column("current_period")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? CurrentPeriod { get; set; }

        [JsonProperty(Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        [Computed]
        public List<Period> Periods { get; set; }

        [Column("periods"), JsonIgnore]
        public string JsonPeriods
        {
            get
            {
                return JsonConvert.SerializeObject(Periods);
            }

            set => Periods = JsonConvert.DeserializeObject<List<Period>>(value);
        }

        [Column("settings"), JsonIgnore]
        public string JsonSettings
        {
            get
            {
                switch (ValueType)
                {
                    case ValueType.BINARY:
                        {
                            return JsonConvert.SerializeObject(BinarySettings);
                        }
                    case ValueType.INTEGER:
                        {
                            return JsonConvert.SerializeObject(IntegerSettings);
                        }
                    case ValueType.INTERVAL:
                        {
                            return JsonConvert.SerializeObject(IntervalSettings);
                        }
                    case ValueType.ENUMS:
                        {
                            return JsonConvert.SerializeObject(EnumSettings);
                        }
                    default:
                        throw new System.Exception("Тип возвращаемого свойства не определен");
                }
               
            }

            set 
            {
                switch (ValueType)
                {
                    case ValueType.BINARY:
                        {
                            BinarySettings = JsonConvert.DeserializeObject<BinaryProperty>(value);
                            break;
                        }
                    case ValueType.INTEGER:
                        {
                            IntegerSettings = JsonConvert.DeserializeObject<IntegerProperty>(value);
                            break;
                        }
                    case ValueType.INTERVAL:
                        {
                            IntervalSettings = JsonConvert.DeserializeObject<IntervalProperty>(value);
                            break;
                        }
                    case ValueType.ENUMS:
                        {
                            EnumSettings = JsonConvert.DeserializeObject<EnumProperty>(value);
                            break;
                        }
                    default:
                        throw new System.Exception("Тип возвращаемого свойства не определен");
                }
               
            }
        }

        [Computed]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BinaryProperty BinarySettings { get; set; }

        [Computed]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public EnumProperty EnumSettings { get; set; }

        [Computed]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IntegerProperty IntegerSettings { get; set; }

        [Computed]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IntervalProperty IntervalSettings { get; set; }
    }
}
