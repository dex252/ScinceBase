using Web.Managers.Strategy;
using Web.Models.Db.Properties;
using Web.Models.Settings;

namespace Web.Managers
{
    public class GenaratorContext: IGenaratorContext
    {
        public IStrategy CurrentStrategy { get; set; }
        public SettingsValues SettingsValues { get; set; }

        public void SetStrategy(Enums.ValueType valueType)
        {
            switch (valueType)
            {
                case Enums.ValueType.BINARY:
                    {
                        CurrentStrategy = new BinaryStrategy(SettingsValues);
                        break;
                    }
                case Enums.ValueType.INTEGER:
                    {
                        CurrentStrategy = new IntegerStrategy(SettingsValues);
                        break;
                    }
                case Enums.ValueType.INTERVAL:
                    {
                        CurrentStrategy = new IntervalStrategy(SettingsValues);
                        break;
                    }
                case Enums.ValueType.ENUMS:
                    break;
                default:
                    break;
            }
        }

        public IProperty GetProperty(int index)
        {
            return CurrentStrategy.GetProperty(index);
        }
    }
}
