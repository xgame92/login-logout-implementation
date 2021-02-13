using System.Net;
using System.Threading.Tasks;
using AuthServer.Helpers.TokenHelper;
using Microsoft.AspNetCore.Http;

namespace AuthServer.Middlewares
{
    public class TokenManagerMiddleware : IMiddleware
    {
        private readonly ITokenHelper _tokenManager;

        public TokenManagerMiddleware(ITokenHelper tokenManager)
        {
            _tokenManager = tokenManager;
        }
    
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await _tokenManager.IsCurrentActiveToken())
            {
                await next(context);
            
                return;
            }
            context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
        }
    }
}