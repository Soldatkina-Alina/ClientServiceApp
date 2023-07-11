using BaseHandler.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseContext.Repository.Implementation
{
    public class BaseRepository<T>
    {
        /// <summary>
        /// Настройки для подключения к БД
        /// </summary>
        public IConfigurationRoot _configuration => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public DbResponce TryAction(Action action)
        {
            try
            {
                using (UserDataBaseContext db = new UserDataBaseContext(_configuration))
                {
                    action();
                }
                return new DbResponce(true);
            }
            catch (Exception ex)
            {
                return new DbResponce(false, ex.Message, ex?.StackTrace ?? "");
            }
        }

        public DbResponce TryFunc(Func<IEnumerable<T>> func)
        {
            try
            {
                var responce = new DbResponce(true);
                using (UserDataBaseContext db = new UserDataBaseContext(_configuration))
                {
                    var result = func();
                    responce.ResultQuery = result;
                }
                
                return responce;
            }
            catch (Exception ex)
            {
                return new DbResponce(false, ex.Message, ex?.StackTrace ?? "");
            }
        }

        public DbResponce TryFunc(Func<T> func)
        {
            try
            {
                var responce = new DbResponce(true);
                using (UserDataBaseContext db = new UserDataBaseContext(_configuration))
                {
                    var result = func();
                    responce.ResultQuery = result;
                }
                return responce;
            }
            catch (Exception ex)
            {
                return new DbResponce(false, ex.Message, ex?.StackTrace ?? "");
            }
        }

        public DbResponce TryFunc(Func<Task<T>> func)
        {
            try
            {
                var responce = new DbResponce(true);
                using (UserDataBaseContext db = new UserDataBaseContext(_configuration))
                {
                    var result = func();
                    responce.ResultQuery = result;
                }
                return responce;
            }
            catch (Exception ex)
            {
                return new DbResponce(false, ex.Message, ex?.StackTrace ?? "");
            }
        }
    }
}
