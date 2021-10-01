using System.Collections.Generic;

namespace Web.Models.Db.Properties
{
    public class IntegerProperty: Property
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
