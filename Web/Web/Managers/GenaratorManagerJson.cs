using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models.Db2.Properties;
using Web.Models.Settings;
using Web.Repositories;

namespace Web.Managers
{
    public class GeneratorManagerJson : IGeneratorManager
    {
        private Random Random { get; }

        private Array EnumsTypes { get; }

        private IGenaratorContext GenaratorContext { get; }

        private ISmartRepository SmartRepository { get; set; }

        public GeneratorManagerJson(IGenaratorContext genaratorContext, ISmartRepository smartRepository)
        {
            Random = new Random();
            EnumsTypes = Enum.GetValues(typeof(Enums.ValueType));
            GenaratorContext = genaratorContext;
            SmartRepository = smartRepository;
        }
        public void GenerateNodes(SettingsValues settings)
        {
            GenaratorContext.SettingsValues = settings;
            GenaratorContext.EnumsValues = SmartRepository.GetAllEnums().ToList();

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
            var propertyType = (Enums.ValueType)EnumsTypes.GetValue(Random.Next(EnumsTypes.Length));
            if (propertyType == Enums.ValueType.ENUMS)
            {
                propertyType = Random.Next(0, 100) > 50 ? Enums.ValueType.INTEGER : Enums.ValueType.BINARY;
            }

            GenaratorContext.SetStrategy(propertyType);

            var property = GenaratorContext.GetProperty(index);
            return default;
            //return new KeyValuePair<Enums.ValueType, IProperty>(propertyType, property);
        }

        List<Models.Db.RootNode> IGeneratorManager.GenerateNodes(SettingsValues settings)
        {
            throw new NotImplementedException();
        }
    }
}
