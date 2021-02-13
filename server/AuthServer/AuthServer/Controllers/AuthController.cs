using System;
using System.Threading;
using System.Threading.Tasks;
using AuthServer.Models;
using AuthServer.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controllers
{
    
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            ThreadSleepRandomly();
            
            var userToLogin = _authService.Login(userForLoginDto);
            
            if (!userToLogin.Success)
            {
                return BadRequest(new ErrorResponseDto(userToLogin.Message));
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(new ErrorResponseDto(userToLogin.Message));
        }
        
        [HttpPost("logout")]
        public async Task<IActionResult> CancelAccessToken()
        {
            ThreadSleepRandomly();
            await _authService.DeactivateCurrentAsync();
 
            return NoContent();
        }
        
        [Authorize]
        [HttpGet("validate")]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }

        private void ThreadSleepRandomly()
        { 
            Random random = new Random();
            var mseconds=random.Next(1, 3) * 1000;   
            Thread.Sleep(mseconds);
        }
    }
}