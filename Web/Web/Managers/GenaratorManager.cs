using System;
using System.Collections.Generic;
using Web.Models.Db.Properties;
using Web.Models.Settings;

namespace Web.Managers
{
    public class GeneratorManager : IGeneratorManager
    {
        private Random Random { get; }

        private Array EnumsTypes { get; }

        private IGenaratorContext GenaratorContext { get; }

        public GeneratorManager(IGenaratorContext genaratorContext)
        {
            Random = new Random();
            EnumsTypes = Enum.GetValues(typeof(Enums.ValueType));
            GenaratorContext = genaratorContext;
        }
        public void GenerateNodes(SettingsValues settings)
        {
            GenaratorContext.SettingsValues = settings;

            //1-Сгенерируем признаки
            var properties = new List<KeyValuePair<Enums.ValueType, IProperty>>();
            for (int i = 0; i < settings.CountOfProperties; i++)
            {
                properties.Add(AddProperty(i));
            }

            //2-Сгенерируем болячки с разными признаками
        }

        private KeyValuePair<Enums.ValueType, IProperty> AddProperty(int index)
        {
            //var propertyType = (Enums.ValueType)EnumsTypes.GetValue(Random.Next(EnumsTypes.Length));
            var propertyType = 50 > Random.Next(100) ? Enums.ValueType.BINARY : Enums.ValueType.INTEGER;
            GenaratorContext.SetStrategy(propertyType);

            var property = GenaratorContext.GetProperty(index);
            return new KeyValuePair<Enums.ValueType, IProperty>(propertyType, property);
        }

        private BinaryProperty GetBinaryProperty()
        {
            throw new NotImplementedException();
        }
    }
}
