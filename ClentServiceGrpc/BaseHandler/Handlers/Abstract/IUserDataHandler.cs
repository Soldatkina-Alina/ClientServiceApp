using BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHandler.Handlers.Abstract
{
    public interface IUserDataHandler
    {
        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Получение пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUserById(int id);

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <returns></returns>
        bool Add(User user);

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <returns></returns>
        bool Remove(User user);

        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <returns></returns>
        bool Update(User user);
    }
}
