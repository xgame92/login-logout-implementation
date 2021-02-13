using System.Collections.Generic;
using AuthServer.Entities;
using AuthServer.Helpers;

namespace AuthServer.Databases.Seeds
{
    public class SeedData
    {
        public static IEnumerable<User> BuildUserSettings()
        {
            return new List<User>
            {
                CreateUser(1,"Yigit", "Tanriverdi", "yigit","D8da1_*"),
                CreateUser(2,"Anastasia", "Mike", "anastasia","D8da1_*"),
                CreateUser(3,"Rebeca", "Rule", "rebeca","D8da1_*"),
                CreateUser(4,"John", "Doe", "john","D8da1_*")
            };
        }

        private static User CreateUser(int id,string firstName,string lastName, string userName, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);
      
            var user = new User
            {
                Id = id,
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            return user;
        }
    }
}