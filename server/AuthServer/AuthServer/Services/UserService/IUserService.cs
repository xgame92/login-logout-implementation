using System.Collections.Generic;
using AuthServer.Entities;

namespace AuthServer.Services.UserService
{
    public interface IUserService
    {
        void Add(User user);
        User GetByMail(string userName);
    }
}