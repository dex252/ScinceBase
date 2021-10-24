using System.Collections.Generic;
using Web.Models.Db;
using Web.Models.Settings;

namespace Web.Managers
{
    public interface IGeneratorManager
    {
        List<RootNode> GenerateNodes(SettingsValues settings);
    }
}
