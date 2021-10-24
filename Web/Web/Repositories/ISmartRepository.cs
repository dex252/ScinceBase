using System.Collections.Generic;
using Web.Models.Db;
using Web.Models.Review;
using Web.ViewModels.Home;

namespace Web.Repositories
{
    public interface ISmartRepository
    {
        /// <summary>
        /// Вернуть обзор на все содержимое таблицы
        /// </summary>
        /// <returns></returns>
        ReviewModel GetReview();

        /// <summary>
        /// Вставляет запись в таблицу и возвращает её id
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int? InsertClass(RootNode item);

        /// <summary>
        /// Получить список всех значений классов
        /// </summary>
        /// <returns></returns>
        IEnumerable<RootNode> GetClasses();

        /// <summary>
        /// Добавить новые перечисленния
        /// </summary>
        /// <param name="enums"></param>
        decimal? InsertNewEnums(EnumsValue enums);

        /// <summary>
        /// Сохранить классы в таблицу
        /// </summary>
        /// <param name="nodes"></param>
        void SaveClasses(List<RootNode> nodes);

        /// <summary>
        /// Получить все перечисления
        /// </summary>
        /// <returns></returns>
        IEnumerable<EnumsValue> GetAllEnums();
    }
}
