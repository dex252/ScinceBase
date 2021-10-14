using Web.Models.Db2.Properties;
using Web.Models.Settings;

namespace Web.Managers
{
    public interface IGenaratorContext
    {
        SettingsValues SettingsValues { get; set; }

        void SetStrategy(Enums.ValueType valueType);
        IProperty GetProperty(int index);
    }
}
