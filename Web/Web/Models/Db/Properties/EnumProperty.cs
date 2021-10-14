﻿using System.Collections.Generic;

namespace Web.Models.Db.Properties
{
    public class EnumProperty
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
