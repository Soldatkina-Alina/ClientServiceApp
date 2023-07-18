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
            return _userRepository.GetAll().Select(x => (User)x).ToList();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public bool Remove(int id)
        {
            var user = _userRepository.GetById(id);
            if(user != null)
            {
                return _userRepository.Delete(user).Success;
            }
            else
            {
                return false;
            }
            
        }

        public bool Update(User user)
        {
            var userFind = GetUserById(user.Id);
            if (userFind != null)
            {
                userFind.Firstname = user.Firstname;
                userFind.Secondname= user.Secondname;
                userFind.Lastname = user.Lastname;
                userFind.Birthdaydate = user.Birthdaydate;
                userFind.Children = user.Children;
                return (_userRepository.Update(userFind)).Success;
            }

            return false;
        }
    }
}
