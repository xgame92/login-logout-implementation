using System.Collections.Generic;
using System.Threading.Tasks;
using AuthServer.Entities;
using AuthServer.Models;

namespace AuthServer.Helpers.TokenHelper
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user);
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync();
        Task DeactivateAsync(string token);
    }
}