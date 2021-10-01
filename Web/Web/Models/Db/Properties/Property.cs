using Dapper;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Web.Models.Db.Periods;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;
using TableAttribute = Dapper.Contrib.Extensions.TableAttribute;
using Web.Extensions;

namespace Web.Models.Db.Properties
{
    [Table("properties")]
    public class Property: IProperty
    {
        [Key]
        public decimal? Id { get; set; }

        [Column("guid")]
        public string Guid { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [JsonProperty(Order = 1)]
        [Column("type")]
        public Enums.ValueType ValueType { get; set; }

        [Column("current_period")]
        public decimal? CurrentPeriod { get; set; }

        [Computed, JsonIgnore]
        public List<IPeriod> Periods { get; set; }

        [JsonProperty(Order = 2)]
        [Column("periods")]
        public string Settings
        {
            get
            {
                return JsonConvert.SerializeObject(Periods);
            }
            set
            {
                Periods = value.DeserializePeriod(ValueType);
            }
        }
    }
}
