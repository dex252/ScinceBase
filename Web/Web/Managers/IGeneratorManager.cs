using Web.Models.Settings;

namespace Web.Managers
{
    public interface IGeneratorManager
    {
        void GenerateNodes(SettingsValues settings);
    }
}
