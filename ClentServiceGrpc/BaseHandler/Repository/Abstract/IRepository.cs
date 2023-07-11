using BaseContext;
using BaseHandler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHandler.Repository.Abstract
{
    public interface IRepository
    {
        IEnumerable<IEntity> GetAll();

        DbResponce Save(IEntity entity);

        DbResponce Delete(IEntity entity);
    }
}
