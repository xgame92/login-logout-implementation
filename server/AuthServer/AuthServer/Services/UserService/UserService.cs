using System.Collections.Generic;
using AuthServer.Entities;
using AuthServer.Repository.UserRepository;

namespace AuthServer.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public void Add(User user)
        {
            _userRepository.Add(user);
        }

        public User GetByMail(string userName)
        {
            return _userRepository.Get(u => u.UserName == userName);
        }
    }
}