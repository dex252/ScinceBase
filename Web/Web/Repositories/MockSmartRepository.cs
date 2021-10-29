using System.Collections.Generic;
using Web.Models.Db;
using Web.Models.Review;

namespace Web.Repositories
{
    public class MockSmartRepository : ISmartRepository
    {
        public IEnumerable<EnumsValue> GetAllEnums()
        {
            var a = "[{\"Id\":1.0,\"Guid\":\"20745cfc-539b-43bd-bfe5-3ccf8b1015fd\",\"Name\":\"Размеры\",\"Values\":[\"Большой\",\"Маленький\",\"Средний\"]},{\"Id\":2.0,\"Guid\":\"e4ba4064-7731-440e-88ad-0e7437dd3f6c\",\"Name\":\"Цвет\",\"Values\":[\"Зеленый\",\"Синий\",\"Желтый\",\"Красный\",\"Белый\",\"Черный\"]},{\"Id\":3.0,\"Guid\":\"3c61ca95-3709-4190-887d-ea11a4819d92\",\"Name\":\"Головная боль\",\"Values\":[\"Да\",\"Нет\"]},{\"Id\":4.0,\"Guid\":\"a20a69b3-c30c-4da8-92d9-0fdd4a8302eb\",\"Name\":\"Температура\",\"Values\":[\"Низкая\",\"Нормальная\",\"Высокая\"]},{\"Id\":5.0,\"Guid\":\"d36fcf0c-4e33-40d7-a9ea-b02a1d4c08ec\",\"Name\":\"Боль в животе\",\"Values\":[\"Да\",\"Нет\"]}]";
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<EnumsValue>>(a);
        }

        public IEnumerable<RootNode> GetClasses()
        {
            return null;
        }

        public ReviewModel GetReview()
        {
            return new ReviewModel()
            {
                CountOfClasses = 20,
                CountOfProperties = 0
            };
        }

        public int? InsertClass(RootNode item)
        {
            return null;
        }

        public decimal? InsertNewEnums(EnumsValue enums)
        {
            return null;
        }

        public void SaveClasses(List<RootNode> nodes)
        {
            return;
        }
    }
}
