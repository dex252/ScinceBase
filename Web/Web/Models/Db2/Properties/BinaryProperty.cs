using System;

namespace Web.Models.Db2.Properties
{
    public class BinaryProperty: PropertyV2
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
