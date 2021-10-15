using Dapper;
using System.Collections.Generic;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;
using TableAttribute = Dapper.Contrib.Extensions.TableAttribute;
using System.Linq;
using Newtonsoft.Json;
using Computed = Dapper.Contrib.Extensions.ComputedAttribute;

namespace Web.Models.Db
{
    [Table("enums")]
    public class EnumsValue
    {
        [Key]
        public decimal? Id { get; set; }

        [Column("guid")]
        public string Guid { get; set; }

        [Column("name")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [Computed]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Values { get; set; }

        [Column("values"), JsonIgnore]
        public string JsonAttributes
        {
            get
            {
                return JsonConvert.SerializeObject(Values);
            }

            set => Values = JsonConvert.DeserializeObject<List<string>>(value);
        }
    }
}
