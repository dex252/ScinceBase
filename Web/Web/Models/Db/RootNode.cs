using Dapper;
using System.Collections.Generic;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;
using TableAttribute = Dapper.Contrib.Extensions.TableAttribute;
using System.Linq;
using Newtonsoft.Json;
using Computed = Dapper.Contrib.Extensions.ComputedAttribute;

namespace Web.Models.Db
{
    [Table("root")]
    public class RootNode
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
        public List<Attribute> Attribures { get; set; }

        [Column("properties"), JsonIgnore]
        public string JsonAttributes { 
            get 
            {
                return JsonConvert.SerializeObject(Attribures); 
            }

            set => Attribures = JsonConvert.DeserializeObject<List<Attribute>>(value);
        }

        public Attribute this[string guid, Attribute attribute]
        {
            get
            {
                return GetOrderAttr(guid);
            }
            set
            {
                SetOrderAttr(guid, attribute);
            }
        }

        public Attribute this[Attribute attribute]
        {
            get
            {
                return GetOrderAttr(attribute.Guid);
            }
            set
            {
                if (string.IsNullOrEmpty(attribute.Guid))
                {
                    throw new System.Exception("Попытка получить атрибут с пустым guid");
                }

                SetOrderAttr(attribute);
            }
        }

        private void SetOrderAttr(string guid, Attribute attributeValue)
        {
            if (Attribures == null)
            {
                Attribures = new List<Attribute>();
            }

            attributeValue.Guid = guid;

            Attribures.Add(attributeValue);
        }

        private void SetOrderAttr(Attribute attributeValue)
        {
            if (Attribures == null)
            {
                Attribures = new List<Attribute>();
            }

            Attribures.Add(attributeValue);
        }

        private Attribute GetOrderAttr(string guid)
        {
            var attr = Attribures.FirstOrDefault(e => e.Guid == guid);
            return attr;
        }
    }
}

