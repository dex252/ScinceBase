using Dapper;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Web.Models.Db2.Periods;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;
using TableAttribute = Dapper.Contrib.Extensions.TableAttribute;
using Web.Extensions;
using Web.Enums;

namespace Web.Models.Db2.Properties
{

    [Table("properties")]
    public class Attribute
    {
        [Key]
        public decimal? Id { get; set; }

        [Column("guid")]
        public string Guid { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("type")]
        public ValueType ValueType { get; set; }

        [Column("current_period")]
        public decimal? CurrentPeriod { get; set; }

        [JsonProperty(Order = 2)]
        [Column("periods")]
        public List<Attribute> Periods { get; set; }
    }
}
