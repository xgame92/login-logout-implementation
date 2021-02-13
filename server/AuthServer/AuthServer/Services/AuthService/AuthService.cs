using System.Threading.Tasks;
using AuthServer.Constants;
using AuthServer.Entities;
using AuthServer.Helpers;
using AuthServer.Helpers.TokenHelper;
using AuthServer.Models;
using AuthServer.Models.Results;
using AuthServer.Services.UserService;

namespace AuthServer.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthService(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.UserName);
            if (userToCheck==null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            
            return new SuccessDataResult<User>(userToCheck,Messages.SuccessfulLogin);
        }
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var accessToken = _tokenHelper.CreateToken(user);
            return new SuccessDataResult<AccessToken>(accessToken,Messages.AccessTokenCreated);
        }

        public async Task DeactivateCurrentAsync()
        {
            await _tokenHelper.DeactivateCurrentAsync();
        }
    }
}