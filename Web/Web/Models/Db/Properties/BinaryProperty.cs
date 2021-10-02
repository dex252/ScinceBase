using System;

namespace Web.Models.Db.Properties
{
    public class BinaryProperty: Property
    {
        /// <summary>
        /// Текущее значение
        /// </summary>
        public bool CurrentValue { get; set; }

        /// <summary>
        /// Нормальное значение
        /// </summary>
        public bool NormalValue { get; set; }
    }
}
