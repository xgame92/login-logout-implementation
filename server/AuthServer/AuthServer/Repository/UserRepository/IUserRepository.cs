using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AuthServer.Entities;

namespace AuthServer.Repository.UserRepository
{
    public interface IUserRepository
    {
        void Add(User user);
        User Get(Expression<Func<User, bool>> filter);
    }
}