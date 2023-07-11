using BaseContext;
using BaseHandler.Common;
using BaseHandler.Handlers.Abstract;
using BaseHandler.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static BaseContext.User;

namespace BaseHandler.Handlers.Conrete
{
    public class ReadUserFromJson : IReadEntityFromFile
    {
        private readonly IRepository _repository;

        public string _path { get; set; }

        public ReadUserFromJson(IRepository repository, string path) {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _path = path?? throw new ArgumentNullException(nameof(path));
        }
 
        /// <summary>
        /// Добавление всех пользователей их файла в БД
        /// </summary>
        /// <returns></returns>
        public bool AddEntity()
        {
            // чтение из файла
            using var stream = File.OpenRead(_path);
            using JsonDocument jsonDocument = JsonDocument.Parse(stream);
            sex result;
            var query = from entity in jsonDocument.RootElement.EnumerateArray()
                        select new User()
                        {
                            Firstname = entity.GetProperty("Firstname").GetString() ?? "",
                            Lastname = entity.GetProperty("Lastname").GetString(),
                            Secondname = entity.GetProperty("Secondname").GetString(),
                            Birthdaydate = new DateOnly(entity.GetProperty("Birthdaydate").GetDateTime().Year, entity.GetProperty("Birthdaydate").GetDateTime().Month, entity.GetProperty("Birthdaydate").GetDateTime().Day),
                            Children = entity.GetProperty("Children").GetBoolean(),
                            //Sex = Enum.TryParse<sex>(entity.GetProperty("Sex").GetString(), true, out result) ? result : null,
                        };
            List<DbResponce> listResponce = new List<DbResponce>();
            var q = query.ToList<User>();
            query.ToList<User>().ForEach(u => listResponce.Add(_repository.Save(u)));

            return listResponce.All(x=> x.Success);
        }

        public bool DeleteEntity()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Чтение всех пользователей из файла
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IEntity> ReadAllEntityFromFile()
        {
            using var stream = File.OpenRead(_path);

            var list = new List<IEntity>();
            sex result;
            using (JsonDocument jsonDocument = JsonDocument.Parse(stream))
            {
                var query = from entity in jsonDocument.RootElement.EnumerateArray()
                            select new User()
                            {
                                Firstname = entity.GetProperty("Firstname").GetString() ?? "",
                                Lastname = entity.GetProperty("Lastname").GetString(),
                                Secondname = entity.GetProperty("Secondname").GetString(),
                                Birthdaydate = DateOnly.FromDateTime(entity.GetProperty("Birthdaydate").GetDateTime().Date), //new DateOnly(entity.GetProperty("Birthdaydate").GetDateTime().Year, entity.GetProperty("Birthdaydate").GetDateTime().Month, entity.GetProperty("Birthdaydate").GetDateTime().Day),
                                Children = entity.GetProperty("Children").GetBoolean(),
                                //Sex = Enum.TryParse<sex>(entity.GetProperty("Sex").GetString(), true, out result) ? result : null,
                            };
                list = query.ToList<IEntity>();
            }
            return list;
        }

        public bool UpdateEntity()
        {
            throw new NotImplementedException();
        }
    }
}
