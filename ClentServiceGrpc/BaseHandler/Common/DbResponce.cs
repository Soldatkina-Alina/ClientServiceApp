using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHandler.Common
{
    public class DbResponce
    {
        /// <summary>
        /// Информация об успешном выполнении запроса
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string MessageError { get; set; }

        /// <summary>
        /// Стек ошибки
        /// </summary>
        public string TraceError { get; set; }

        /// <summary>
        /// Результат выполнения запроса к БД
        /// </summary>
        public object ResultQuery { get; set; }

        public DbResponce(bool succes): this (succes, string.Empty, string.Empty){ }

        public DbResponce(bool succes, string messageError, string traceError)
        {
            this.Success = succes;
            this.MessageError = messageError;
            this.TraceError = traceError;
        }
    }
}
