using System.Collections.Generic;
using Web.Enums;

namespace Web.Models.Db2.Properties
{
    public class EnumProperty: PropertyV2
    {
        public int? EnumId { get; set; }

        /// <summary>
        /// Текущие значения
        /// </summary>
        public List<string> CurrentValues { get; set; }

        /// <summary>
        /// Нормальные значения
        /// </summary>
        public List<string> NormalValues { get; set; }
    }
}
