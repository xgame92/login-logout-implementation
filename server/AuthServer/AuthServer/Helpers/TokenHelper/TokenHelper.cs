using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthServer.Entities;
using AuthServer.Extensions;
using AuthServer.Models;
using AuthServer.Models.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer.Helpers.TokenHelper
{
    public class TokenHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private readonly TokenOptions _tokenOptions;
        private readonly DateTime _accessTokenExpiration;
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenHelper(
            IConfiguration configuration,
            IMemoryCache cache,
            IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        }

        public async Task<bool> IsCurrentActiveToken()
            => await IsActiveAsync(GetCurrentAsync());

        public async Task DeactivateCurrentAsync()
            => await DeactivateAsync(GetCurrentAsync());

        public async Task<bool> IsActiveAsync(string token)
            => await Task.FromResult(_cache.Get(token) == null);

        public async Task DeactivateAsync(string token)
        {
            _cache.Set(token, DateTime.Now, TimeSpan.FromDays(1));
            await Task.CompletedTask;
        }

        public AccessToken CreateToken(User user)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,
            SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: new List<Claim>(),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }

        private static string GetKey(string token)
            => $"tokens:{token}:deactivated";
    }
}