using System.Security.Cryptography;
using System.Text;
using BusBookTicket.Auth.DTOs.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BusBookTicket.Auth.Security
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private AccResponse response;
        static SHA256 sha256 = SHA256.Create();

        
        public JwtMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public static void ConfigureService (IServiceCollection services) 
        {
            var secretBytes = Encoding.UTF8.GetBytes("BachelorOfEngineeringThesisByMinhDung");
            var symmetricKey = sha256.ComputeHash(secretBytes);
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                        ClockSkew = TimeSpan.Zero,
                    };

                    options.RequireHttpsMetadata = false; // Set to true if you require HTTPS
                    options.SaveToken = true;
                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }

                    };
                });
            // string token = JwtUtils.GetToken(context);
            // if (!string.IsNullOrEmpty(token))
            // {
            //     var principal = JwtUtils.GetPrincipal(token);
            //     string username = principal.Claims.ElementAt(1).Value;
            //     string roleName = principal.Claims.ElementAt(2).Value;
            //     using (var scope = _serviceScopeFactory.CreateScope())
            //     {
            //         var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
            //         // Use the scoped service within the scope of the request
            //         response = await authService.getAccByUsername(username, roleName);
            //         // Check account exist in data
            //         if (response.roleName != principal.Claims.ElementAt(2).Value ||
            //             response.userID.ToString() != principal.Claims.ElementAt(0).Value||
            //             response.status != (int)EnumsApp.Active)
            //             throw new AuthException(AuthConstants.AUTHENRIZATION);
            //     }
            // }
            //
            // await _next.Invoke(context);
        }
    }
}
