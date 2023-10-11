using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Exceptions;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Auth.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BusBookTicket.Auth.Security
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private AccResponse response;
        public JwtMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = JwtUtils.GetToken(context);
            if (!string.IsNullOrEmpty(token))
            {
                var principal = JwtUtils.GetPrincipal(token);
                string username = principal.Claims.ElementAt(1).Value;
                string roleName = principal.Claims.ElementAt(2).Value;
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
                    // Use the scoped service within the scope of the request
                    response = _authService.getAccByUsername(username, roleName);
                    // Check account exist in data
                    if (response.roleName != principal.Claims.ElementAt(2).Value ||
                        response.userID.ToString() != principal.Claims.ElementAt(0).Value)
                        throw new AuthException(AuthConstants.UNAUTHORIZATION);
                }
            }

            await _next.Invoke(context);
        }
    }
}
