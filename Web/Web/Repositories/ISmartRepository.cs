using System.Collections.Generic;
using Web.Models.Db;
using Web.ViewModels.Home;

namespace Web.Repositories
{
    public interface ISmartRepository
    {
        /// <summary>
        /// Вернуть обзор на все содержимое таблицы
        /// </summary>
        /// <returns></returns>
        ReviewViewModel GetReview();

        /// <summary>
        /// Вставляет запись в таблицу и возвращает её id
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int? InsertClass(Classes item);

        /// <summary>
        /// Получить список всех значений классов
        /// </summary>
        /// <returns></returns>
        IEnumerable<Classes> GetClasses();
    }
}
