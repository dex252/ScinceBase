using System.Collections.Generic;
using Web.Models.Db;
using Web.Models.Settings;

namespace Web.Managers
{
    public interface IGenaratorContext
    {
        SettingsValues SettingsValues { get; set; }

        List<EnumsValue> EnumsValues { get; set; }

        void SetStrategy(Enums.ValueType valueType);

        Attribute GetProperty(int index);
    }
}
