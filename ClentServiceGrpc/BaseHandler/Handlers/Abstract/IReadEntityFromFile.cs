using BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHandler.Handlers.Abstract
{
    public interface IReadEntityFromFile
    {
        public string _path { get; set; }
        /// <summary>
        /// Добавление всех сущностей из файла в БД
        /// </summary>
        public bool AddEntity();

        /// <summary>
        /// Обновление всех сущностей из файла в БД
        /// </summary>
        public bool UpdateEntity();

        /// <summary>
        /// Удаление всех сущностей из файла из БД
        /// </summary>
        public bool DeleteEntity();

        /// <summary>
        /// Чтение из сущностей из файла
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IEntity> ReadAllEntityFromFile();
    }
}
