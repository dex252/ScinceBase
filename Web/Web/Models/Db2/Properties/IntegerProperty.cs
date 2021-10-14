using System.Collections.Generic;

namespace Web.Models.Db2.Properties
{
    public class IntegerProperty: PropertyV2
    {
        /// <summary>
        /// Текущее значение
        /// </summary>
        public int CurrentValue { get; set; }

        /// <summary>
        /// Нормальные значения
        /// </summary>
        public List<int> NormalValue { get; set; }
    }
}
