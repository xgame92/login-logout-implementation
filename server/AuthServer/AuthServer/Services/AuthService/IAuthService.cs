using System.Threading.Tasks;
using AuthServer.Entities;
using AuthServer.Models;
using AuthServer.Models.Results;

namespace AuthServer.Services.AuthService
{
    public interface IAuthService
    {
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        
        IDataResult<AccessToken> CreateAccessToken(User user);
        Task DeactivateCurrentAsync();
    }
}