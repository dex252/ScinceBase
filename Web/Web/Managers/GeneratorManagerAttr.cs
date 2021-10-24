using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models.Db;
using Web.Models.Settings;
using Web.Repositories;
using Attribute = Web.Models.Db.Attribute;

namespace Web.Managers
{
    public class GeneratorManagerAttr : IGeneratorManager
    {
        private Random Random { get; }

        private Array EnumsTypes { get; }

        private IGenaratorContext GenaratorContext { get; }

        private ISmartRepository SmartRepository { get; set; }

        public GeneratorManagerAttr(IGenaratorContext genaratorContext, ISmartRepository smartRepository)
        {
            GenaratorContext = genaratorContext;
            Random = new Random();
            EnumsTypes = Enum.GetValues(typeof(Enums.ValueType));
            SmartRepository = smartRepository;
        }

        public List<RootNode> GenerateNodes(SettingsValues settings)
        {
            GenaratorContext.SettingsValues = settings;
            GenaratorContext.EnumsValues = SmartRepository.GetAllEnums().ToList();

            //1-Сгенерируем признаки (одинаковые для всех, но текущие значения у них различаются)
            var properties = new List<Attribute>(settings.CountOfProperties);
            for (int i = 0; i < settings.CountOfProperties; i++)
            {
                properties.Add(AddProperty(i));
            }

            //2-Генерируем классы на основе свойств
            var nodes = new List<RootNode>(settings.CountOfClasses);
            var postfix = Guid.NewGuid().ToString();
            for (int i = 0; i < settings.CountOfClasses; i++)
            {
                nodes.Add(AddNode(i, properties, postfix));
            }

            //TODO: что делать с нормальными занчениями, когда число периодов = 1??? Для булевых период всегда будет нормальным

            return nodes;
        }

        private RootNode AddNode(int index, List<Attribute> properties, string postfix)
        {
            var node = new RootNode();
            node.Name = $"node-{index}-{postfix}";
            node.Guid = Guid.NewGuid().ToString();
            node.Attribures = properties;

            return node;
        }

        private Attribute AddProperty(int index)
        {
            var propertyType = (Enums.ValueType)EnumsTypes.GetValue(Random.Next(EnumsTypes.Length));

            GenaratorContext.SetStrategy(propertyType);

            var property = GenaratorContext.GetProperty(index);
            return property;
        }
    }
}
