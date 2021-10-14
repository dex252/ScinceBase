using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Db2.Properties;

namespace Web.Managers.Strategy
{
    public interface IStrategy
    {
        IProperty GetProperty(int index);
    }
}
