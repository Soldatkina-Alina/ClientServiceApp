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
        /// Добавление пользателя
        /// </summary>
        /// <param name="user">Пользователь</param>
        public DbResponce Update(IEntity entity)
        {
            var user = (User)entity;
            return TryAction(async () =>
            {
                using (UserDataBaseContext db = new UserDataBaseContext(_configuration))
                {
                    var userFind = db.Users.Find(user.Id);
                    if (userFind != null)
                    {
                        userFind.Firstname = user.Firstname;
                        userFind.Secondname = user.Secondname;
                        userFind.Lastname = user.Lastname;
                        userFind.Birthdaydate = user.Birthdaydate;
                        userFind.Children = user.Children;
                       
                    }
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

            var r = users;
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
            User userFind = null;

            var result = TryFunc(async ()=>
            {
                using (UserDataBaseContext db = new UserDataBaseContext(_configuration))
                {
                    return await db.Users.FindAsync(id);
                }
            });

            if (result != null && result.ResultQuery != null)
            {
                userFind = (User)(((Task<User>)(result.ResultQuery)).Result);
            }

            return userFind;
        }
    }
}
