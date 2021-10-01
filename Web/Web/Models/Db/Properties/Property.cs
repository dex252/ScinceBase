using Dapper;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Web.Models.Db.Periods;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;
using TableAttribute = Dapper.Contrib.Extensions.TableAttribute;

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

        [Column("type")]
        public Enums.ValueType ValueType { get; set; }

        [Column("current_period")]
        public decimal? CurrentPeriod { get; set; }

        [Computed]
        public List<IPeriod> Periods { get; set; }

        [Column("periods")]
        public string Settings
        {
            get
            {
                return JsonConvert.SerializeObject(Periods);
            }
            set
            {
                Periods = JsonConvert.DeserializeObject<List<IPeriod>>(value);
            }
        }
    }
}
