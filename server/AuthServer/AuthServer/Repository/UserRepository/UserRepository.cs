using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AuthServer.Databases.Contexts;
using AuthServer.Entities;

namespace AuthServer.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthServerDbContext _context;

        public UserRepository(AuthServerDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public User Get(Expression<Func<User, bool>> filter)
        {
            return _context.Set<User>().SingleOrDefault(filter);
        }
    }
}