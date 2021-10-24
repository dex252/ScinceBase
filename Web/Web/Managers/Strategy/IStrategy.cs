using Web.Models.Db;

namespace Web.Managers.Strategy
{
    public interface IStrategy
    {
        Attribute GetProperty(int index);
    }
}
