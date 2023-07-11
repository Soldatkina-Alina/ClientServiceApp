using BaseContext.Repository.Interface;
using BaseContext.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseContext;
using Microsoft.Extensions.Configuration;
using BaseHandler.Common;
using BaseHandler.Repository.Abstract;

namespace BaseHandler.Repository.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        /// <summary>
        /// Добавление пользателя
        /// </summary>
        /// <param name="user">Пользователь</param>
        public DbResponce Save(IEntity user)
        {
            return TryAction(async () =>
            {
                using (UserDataBaseContext db = new UserDataBaseContext(_configuration))
                {
                    await db.Users.AddAsync((User)user);
                    await db.SaveChangesAsync();
                }
            });
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        public DbResponce Delete(IEntity user)
        {
            return TryAction(async () =>
            {
                using (UserDataBaseContext db = new UserDataBaseContext(_configuration))
                {
                    db.Users.Remove((User)user);
                    await db.SaveChangesAsync();
                }
            });
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns>Лист пользователей</returns>
        public IEnumerable<IEntity> GetAll()
        {
            //Func<IEnumerable<User>> f = () =>
            //{
            //    using (UserDataBaseContext db = new UserDataBaseContext(_configuration))
            //    {
            //        var users = db.Users.ToList();
            //        return users;
            //    }
            //};

            IEnumerable<User> users = new List<User>();

            var result = TryFunc(() =>
            {
                using (UserDataBaseContext db = new UserDataBaseContext(_configuration))
                {
                    return db.Users.ToList();
                }
            });

            if (result.ResultQuery != null)
            {
                users = (IEnumerable<User>)result.ResultQuery;
            }

            return users;
        }

        /// <summary>
        /// Нахождение пользователя по id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>Пользователь</returns>
        /// <exception cref="NotImplementedException"></exception>
        public User GetById(int id)
        {
            User userFind = new User();

            var result = TryFunc(async ()=>
            {
                using (UserDataBaseContext db = new UserDataBaseContext(_configuration))
                {
                    var user = await db.Users.FindAsync(id);
                    return user ?? new User();
                }
            });

            if (result.ResultQuery != null)
            {
                userFind = (User)result.ResultQuery;
            }

            return userFind;
        }
    }
}
