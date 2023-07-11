using BaseHandler.Common;
using BaseHandler.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseContext.Repository.Interface
{
    public interface IUserRepository: IRepository
    {
        User GetById(int id);
    }
}
