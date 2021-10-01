using Dapper;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Web.Models.Db.Properties;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;
using TableAttribute = Dapper.Contrib.Extensions.TableAttribute;
using Web.Extensions;

namespace Web.Models.Db
{
    [Table("root")]
    public class Classes
    {
        [Key]
        public decimal? Id { get; set; }

        [Column("guid")]
        public string Guid { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Computed, JsonIgnore]
        public List<KeyValuePair<Enums.ValueType, IProperty>> Properties { get; set; }
        
        [Column("properties")]
        public string Settings
        {
            get
            {
                return JsonConvert.SerializeObject(Properties);
            }
            set
            {
                Properties = value.DeserializeProperty();
            }
        }
    }
}
