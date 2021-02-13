using System;
using AutoMapper;
using AuthServer.Databases.Contexts;
using AuthServer.Helpers;
using AuthServer.Helpers.TokenHelper;
using AuthServer.Middlewares;
using AuthServer.Models.Jwt;
using AuthServer.Repository.UserRepository;
using AuthServer.Services.AuthService;
using AuthServer.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AuthServerDbContext>(options =>
                options.UseNpgsql(GetPostgreSqlConnectionString(configuration),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30),
                            new string[0]);
                    }));
            
            return services;
        }
        public static IServiceCollection AddJwtOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });
            
            return services;
        }
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient<TokenManagerMiddleware>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenHelper, TokenHelper>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

        private static string GetPostgreSqlConnectionString(IConfiguration configuration)
        {
            return
                $"Server={configuration["DATABASE:HOST"]};Port={configuration["DATABASE:PORT"]};Database={configuration["DATABASE:NAME"]};" +
                $"UserName={configuration["DATABASE:USER"]};Password={configuration["DATABASE:PASSWORD"]}";
        }
    }
}