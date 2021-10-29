using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Managers.Strategy;
using Web.Models.Db;

namespace Web.Managers
{
    public class BusinessManager
    {
        public static List<RootNode> Nodes { get; set; }
    }
}
