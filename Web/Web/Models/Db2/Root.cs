using Dapper;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Web.Models.Db2.Properties;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;
using TableAttribute = Dapper.Contrib.Extensions.TableAttribute;
using Web.Extensions;
using System.Linq;

namespace Web.Models.Db2
{
    [Table("root")]
    public class ClassesV2
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

    [Table("root")]
    public class Classes
    {
        [Key]
        public decimal? Id { get; set; }

        [Column("guid")]
        public string Guid { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("properties")]
        public List<Attribute> Attribures { get; set; }

        public Attribute this [string guid, string attributeValue]
        {
            get
            {
                return GetOrderAttr(guid);
            }
            set
            {
                SetOrderAttr(guid, attributeValue);
            }
        }

        private void SetOrderAttr(string guid, string attributeValue)
        {
           if(Attribures == null)
           {
                Attribures = new List<Attribute>();
           }

           //Добавить все параметры атрибута
            var attr = new Attribute();
            attr.Guid = guid;

            Attribures.Add(attr);
        }

        private Attribute GetOrderAttr(string guid)
        {
            var attr = Attribures.FirstOrDefault(e => e.Guid == guid);
            return attr;
        }
    }
}
