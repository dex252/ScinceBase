using Dapper;
using Dapper.Contrib.Extensions;
using System.Xml;

namespace Web.Models.Db
{
    [Dapper.Table("root")]
    public class Classes
    {
        [Dapper.Key]
        public decimal? Id { get; set; }

        [Column("guid")]
        public string Guid { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Computed]
        public string Json { get; set; }

        [Column("json")]
        public XmlCDataSection CData
        {
            get
            {
                return new XmlDocument().CreateCDataSection(Json);
            }
            set
            {
                Json = value.Value;
            }
        }

    }
}
