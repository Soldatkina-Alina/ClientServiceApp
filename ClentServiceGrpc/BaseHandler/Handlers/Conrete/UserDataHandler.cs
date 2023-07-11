using BaseContext;
using BaseContext.Repository.Interface;
using BaseHandler.Handlers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHandler.Handlers.Conrete
{
    public class UserDataHandler : IUserDataHandler
    {
        private readonly IUserRepository _userRepository;

        public UserDataHandler(IUserRepository userRepository) {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public bool Add(User user)
        {
            return (_userRepository.Save(user)).Success;
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(User user)
        {
            throw new NotImplementedException();
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
